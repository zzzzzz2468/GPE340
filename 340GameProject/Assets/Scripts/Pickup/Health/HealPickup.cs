using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickup : BasePickup
{
    //checks if the object collides with the player, and then changes the players health
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 8) return;

        Health health = other.GetComponent<Health>();

        if (health == null) return;
        if (health.CurHealth == health.MaxHealth) return;

        health.onHeal.Invoke();
        base.OnPickUp();
        //OnPickUp(health);
    }

    //protected override void OnPickUp(Health health)
    //{
    //    health.Heal(10);
    //    base.OnPickUp(health);
    //}
}