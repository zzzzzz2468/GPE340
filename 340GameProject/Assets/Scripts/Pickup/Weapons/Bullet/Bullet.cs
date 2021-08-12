using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 8;

    //checks if the bullet collides with an enemy or player
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7 || collision.gameObject.layer == 8)
        {
            if (collision.gameObject.GetComponent<Health>() != null)
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}