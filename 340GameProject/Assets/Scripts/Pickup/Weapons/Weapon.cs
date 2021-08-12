using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    //sets teh variables for everything that is needed
    public bool isTriggerPulled = false;
    public Transform rightHandPoint;
    public Transform leftHandPoint;

    private float timeBetweenShots = 0;
    [SerializeField] private float dropForce = 30;
    [SerializeField] private float shotsPerMinute = 260f;

    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject barrel;
    [SerializeField] protected float bulletVelocity = 5f;
    [SerializeField] protected int maxAmmo = 30;
    protected int totalAmmo;
    protected int curAmmo;
    protected bool isReloading;
    [HideInInspector] public GameObject container;

    protected Rigidbody rb;

    //public UnityEvent OnMainActionStart;
    //public UnityEvent OnMainActionEnd;
    //public UnityEvent OnMainActionHold;

    public virtual void Start()
    {
        curAmmo = maxAmmo;
        totalAmmo = maxAmmo * 2;
    }

    //checks if the trigger is pulled to shoot
    public virtual void Update()
    {
        if (isTriggerPulled)
        {
            if (Time.time > timeBetweenShots)
            {
                OnTriggerHold();
                timeBetweenShots += 60f / shotsPerMinute;
            }
        }
        else if (Time.time > timeBetweenShots)
        {
            timeBetweenShots = Time.time;
        }
    }

    //picks the weapon up
    public void PickUp(float rotation)
    {
        rb = GetComponent<Rigidbody>();
        transform.SetParent(container.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0, rotation, 0);
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
    }

    //drops the weapon to the ground
    public void Drop()
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.AddForce(transform.forward * dropForce);
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
        gameObject.layer = 6;
    }

    //sets variables
    public abstract void OnTriggerPull();
    public abstract void OnTriggerRelease();
    public abstract void OnTriggerHold();
}