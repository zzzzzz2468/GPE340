using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolWeapon : Weapon
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
        //shoots a bullet out from the barrel and changes the layer so it doesn't collides
        curAmmo -= 1;
        var projectile = Instantiate(bullet, barrel.transform.position, Quaternion.Euler(barrel.transform.rotation.x, barrel.transform.rotation.y + 90, barrel.transform.rotation.z));
        projectile.GetComponent<Rigidbody>().AddRelativeForce(-transform.right * bulletVelocity, ForceMode.VelocityChange);
        projectile.gameObject.layer = gameObject.layer;
    }

    //reloads the gun
    public IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(2);
        var temp = maxAmmo - curAmmo;
        if (totalAmmo >= temp)
        {
            curAmmo += temp;
            totalAmmo -= temp;
        }
        else if(totalAmmo < temp)
        {
            curAmmo += totalAmmo;
            totalAmmo = 0;
        }
        isReloading = false;
    }

    public override void OnTriggerHold()
    {
        //Shoot();
    }

    //shoots when the player pulls the trigger
    public override void OnTriggerPull()
    {
        if (curAmmo > 0)
            Shoot();
        else if(curAmmo <= 0 && !isReloading)
            StartCoroutine(Reload());
    }

    public override void OnTriggerRelease()
    {
        //isTriggerPulled = false;
    }
}