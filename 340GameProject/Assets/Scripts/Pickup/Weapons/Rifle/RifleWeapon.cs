using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleWeapon : Weapon
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public void Shoot()
    {
        //shoots a bullet out from the barrel and changes the layer so it doesn't collide
        var projectile = Instantiate(bullet, barrel.transform.position, Quaternion.Euler(barrel.transform.rotation.x, barrel.transform.rotation.y + 90, barrel.transform.rotation.z));
        projectile.GetComponent<Rigidbody>().AddRelativeForce(transform.right * bulletVelocity, ForceMode.VelocityChange);
        projectile.gameObject.layer = gameObject.layer;
    }

    public override void OnTriggerHold()
    {
        Shoot();
    }

    //sets the trigger to true so that it is automatic
    public override void OnTriggerPull()
    {
        isTriggerPulled = true;
    }

    public override void OnTriggerRelease()
    {
        isTriggerPulled = false;
    }
}