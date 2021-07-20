using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Vector3 offset = new Vector3(0, 10, 0);

    //follows target at set position and speed
    private void FixedUpdate()
    {
        if (target == null) return;

        Vector3 goTo = target.position + offset;
        Vector3 smoothGoTo = Vector3.Lerp(transform.position, goTo, speed);
        transform.position = smoothGoTo;
    }
}