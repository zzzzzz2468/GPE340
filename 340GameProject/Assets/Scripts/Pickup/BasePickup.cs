using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePickup : MonoBehaviour
{
    protected virtual void OnPickUp()
    {
        Destroy(gameObject);
    }
}