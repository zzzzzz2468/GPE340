using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnData : MonoBehaviour
{
    //variables needed for all pawns, characters and AI
    protected Animator anim;

    [Header("Speeds")]
    [SerializeField] protected float speed = 4;
    [SerializeField] protected float turnSpeed = 180;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
}