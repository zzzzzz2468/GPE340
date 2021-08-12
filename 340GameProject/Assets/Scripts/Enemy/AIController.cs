using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Pawn
{
    //variables needed for controlling the AI
    [HideInInspector] public NavMeshAgent agent;
    private Transform followTarget;
    [SerializeField] private float decisionDelay = 0.2f;
    [SerializeField] private float shootDelayTime = 0.5f;
    private float nextDecisionTime;
    private float shootDelay;
    private GameManager gameManager;
    public Weapon weapon;
    [SerializeField] protected List<GameObject> weapons = new List<GameObject>();
    [SerializeField] protected GameObject rifleContainer;
    protected bool weaponEquipped = false;
    private bool shooting;

    void Start()
    {
        //sets varaibles
        gameManager = GameManager.instance;
        agent = GetComponent<NavMeshAgent>();
        nextDecisionTime = Time.time;
        shootDelay = Time.time;

        //equips a random weapon
        var weaponType = Random.Range(0, weapons.Count);

        string name = "";
        switch(weaponType)
        {
            case 0:
                name = "Pistol";
                break;
            case 1:
                name = "Rifle";
                break;
            default:
                Debug.LogError("NotAWeapon: AICONTROLLER");
                break;
        }

        EquipWeapon(name);
    }

    void Update()
    {
        //updates the target positon of the nav mesh
        if (Time.time >= nextDecisionTime)
        {
            //is there a target
            if (followTarget == null)
            {
                followTarget = GetFollowTarget();
                if(followTarget == null)
                {
                    agent.isStopped = true;
                    anim.SetFloat("Forward", 0.0f);
                    anim.SetFloat("Right", 0.0f);
                    return;
                }
            }

            if (!agent.isActiveAndEnabled) return;

            agent.SetDestination(followTarget.position);
            nextDecisionTime = Time.time + decisionDelay;
        }

        //shoots the gun
        if (agent.pathStatus == NavMeshPathStatus.PathComplete && !shooting)
            StartCoroutine(ShootGun());
        else
            weapon.OnTriggerRelease();

        //gets the desired movement and inverses it so it becomes usable
        Vector3 desiredMovement = agent.desiredVelocity;
        desiredMovement = transform.InverseTransformDirection(desiredMovement);
        desiredMovement = desiredMovement.normalized;
        desiredMovement *= speed;

        //using root motion
        anim.SetFloat("Forward", desiredMovement.z);
        anim.SetFloat("Right", desiredMovement.x);
    }

    //shoots the gun on delay
    IEnumerator ShootGun()
    {
        shooting = true;

        yield return new WaitForSeconds(shootDelayTime);

        if (weapon.name.Contains("Rifle"))
            weapon.OnTriggerHold();
        else if (weapon.name.Contains("Pistol"))
            weapon.OnTriggerPull();

        shooting = false;
    }

    //sets the follow target
    public Transform GetFollowTarget()
    {
        Transform target = null;

        if(gameManager.player != null)
            target = gameManager.player.transform;

        return target;
    }

    private void OnAnimatorMove()
    {
        agent.velocity = anim.velocity;
    }

    public void EquipWeapon(string name)
    {
        //destroys the weapon on the ground and spawns a new one in
        weaponEquipped = true;

        //runs through all the weapons and finds the corresponding weapon
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].name.Contains(name.Substring(0, 5)))
            {
                var tempWeapon = Instantiate(weapons[i]);
                tempWeapon.GetComponent<Weapon>().container = rifleContainer;

                //rifle and pistol are flipped, so i changed the rotation manually
                if (name.Contains("Rifle")) tempWeapon.GetComponent<Weapon>().PickUp(180);
                else if (name.Contains("Pistol")) tempWeapon.GetComponent<Weapon>().PickUp(0);

                weapon = tempWeapon.gameObject.GetComponent<Weapon>();
                tempWeapon.gameObject.layer = gameObject.layer;
                break;
            }
        }
    }

    public void OnAnimatorIK(int layerIndex)
    {
        if (weapon != null)
        {
            //changes where the player is looking when holding a weapon
            if (weapon.gameObject.name.Contains("Rifle"))
                anim.SetLookAtPosition(weapon.transform.position + (-10 * weapon.transform.forward));
            else
                anim.SetLookAtPosition(weapon.transform.position + (10 * weapon.transform.forward));
            anim.SetLookAtWeight(1);

            //sets the right hand postiton
            if (weapon.rightHandPoint != null)
            {
                anim.SetIKPosition(AvatarIKGoal.RightHand, weapon.rightHandPoint.position);
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                anim.SetIKRotation(AvatarIKGoal.RightHand, weapon.rightHandPoint.rotation);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            }

            //sets the left hand position
            if (weapon.leftHandPoint != null)
            {
                anim.SetIKPosition(AvatarIKGoal.LeftHand, weapon.leftHandPoint.position);
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                anim.SetIKRotation(AvatarIKGoal.LeftHand, weapon.leftHandPoint.rotation);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            }
        }
        //resets the iks
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            anim.SetLookAtWeight(0);
        }
    }
}