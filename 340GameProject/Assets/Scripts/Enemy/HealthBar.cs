using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Vector3 localScale;

    //gets the local scale
    private void Start()
    {
        localScale = transform.localScale;
    }

    //changes the local scale of the health bar
    public void UpdateSlider(float curHealth)
    {
        localScale.x = curHealth;
        transform.localScale = localScale;
    }
}