using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerData
{
    //the player controller, functions and controls for the player

    private void Update()
    {
        //Get direction of stick / keys
        Vector3 stickDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //Clamps the direction to 1
        stickDirection = Vector3.ClampMagnitude(stickDirection, 1);

        //Invert movement so its world based
        Vector3 animationDirection = transform.InverseTransformDirection(stickDirection);

        //Pass the floats into animator for animation
        anim.SetFloat("Forward", animationDirection.z * speed);
        anim.SetFloat("Right", animationDirection.x * speed);

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        //Rotate to face mouse
        RotateToMousePointer();

        //deal damage to yourself and drop
        if (Input.GetKeyDown(KeyCode.R))
            GetComponent<Health>().TakeDamage(10);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            weapon.Drop();
            weapon = null;
        }

        //checks if there is a weapons
        if (weapon == null) return;

        //triggers the weapon
        if (Input.GetButtonDown("Fire1"))
        {
            weapon.OnTriggerPull();
            //weapon.OnMainActionStart.Invoke;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            weapon.OnTriggerRelease();
        }
    }

    public void RotateToMousePointer()
    {
        //Find the ground
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        //Draw a ray from mouse on screen to world space
        Ray rayMousePosition = playerCam.ScreenPointToRay(Input.mousePosition);

        //Using diarance of ground and ray, find the point
        float distance;
        groundPlane.Raycast(rayMousePosition, out distance);
        Vector3 targetPoint = rayMousePosition.GetPoint(distance);

        //Rotate towards the point
        RotateTowards(targetPoint);
    }

    //Adds root motion up
    private void OnAnimatorMove()
    {
        rootMotion += anim.deltaPosition;
    }

    private void FixedUpdate()
    {
        if(isJumping) //player is in air
            UpdateInAir();
        else //player is grounded
            UpdateOnGround();
    }

    //applies rootmotion when the player is on the ground
    private void UpdateOnGround()
    {
        Vector3 stepForwardAmount = rootMotion * groundSpeed;
        Vector3 stepDownAmount = Vector3.down * stepDown;

        charCont.Move(stepForwardAmount + stepDownAmount);
        rootMotion = Vector3.zero;

        if (!charCont.isGrounded)
            SetInAir(0);
    }

    //disables the movement root motion while having the player jump
    private void UpdateInAir()
    {
        velocity.y -= gravity * Time.fixedDeltaTime;
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        //displacement += CalculateAirControl();
        charCont.Move(displacement);
        isJumping = !charCont.isGrounded;
        rootMotion = Vector3.zero;
        anim.SetBool("IsJumping", isJumping);
    }

    //tells the player to jump
    private void Jump()
    {
        if(!isJumping)
        {
            float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpheight);
            SetInAir(jumpVelocity);
        }
    }

    //sets the animation and velocity
    private void SetInAir(float jumpVelocity)
    {
        isJumping = true;
        velocity = anim.velocity * jumpDamp * groundSpeed;
        velocity.y = jumpVelocity;
        anim.SetBool("IsJumping", true);
    }

    //Vector3 CalculateAirControl()
    //{
    //    return ((transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal")) * (airControl/100));
    //}

    //allows for moving different objects with a charcontroller
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        //whether or not the object can be moved
        if (body == null || body.isKinematic)
            return;

        //prevents certain obejcts from being pushed
        if (hit.moveDirection.y < -0.3f)
            return;

        //calculates the push direction
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        //apply push force
        body.velocity = pushDir * pushPower;
    }

    //Equip the weapon when the player is in the trigger box
    private void OnTriggerStay(Collider other)
    {
        if (weapon != null) return;

        if(other.gameObject.layer == 6 && Input.GetKeyDown(KeyCode.E))
            EquipWeapon(other.gameObject.name, other.gameObject);
    }

    //runs the code to equip the weapon
    public void EquipWeapon(string name, GameObject other)
    {
        //destroys the weapon on the ground and spawns a new one in
        weaponEquipped = true;
        Destroy(other);

        //runs through all the weapons and finds the corresponding weapon
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].name.Contains(name.Substring(0, 5)))
            {
                var tempWeapon = Instantiate(weapons[i]);
                tempWeapon.GetComponent<Weapon>().container = rifleContainer;

                //rifle and pistol are flipped, so i changed the rotation manually
                if(name.Contains("Rifle")) tempWeapon.GetComponent<Weapon>().PickUp(180);
                else if(name.Contains("Pistol")) tempWeapon.GetComponent<Weapon>().PickUp(0);

                weapon = tempWeapon.gameObject.GetComponent<Weapon>();
                tempWeapon.gameObject.layer = gameObject.layer;
                break;
            }
        }
    }

    public void AddToScore(float points)
    {

    }

    //sets up iks so the player can carry weapons correctly
    public void OnAnimatorIK(int layerIndex)
    {
        if (weapon != null)
        {
            //changes where the player is looking when holding a weapon
            if(weapon.gameObject.name.Contains("Rifle"))
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