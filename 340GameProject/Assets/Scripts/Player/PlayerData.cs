using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Pawn
{
    //data needed for player characters, but not ai

    [Header("Camera")]
    [SerializeField] protected Camera playerCam;

    [Header("Jumping / Falling")]
    [SerializeField] protected float jumpheight = 3f;
    [SerializeField] protected float gravity = 20f;
    [SerializeField] protected float stepDown = 0.1f;
    [SerializeField] protected float airControl = 2.5f;
    [SerializeField] protected float jumpDamp = 2.5f;

    [SerializeField] protected float groundSpeed = 1f;
    [SerializeField] protected float pushPower = 2.0F;

    protected Vector3 rootMotion;
    protected Vector3 velocity;
    protected bool isJumping;
    protected CharacterController charCont;

    private void Start()
    {
        charCont = GetComponent<CharacterController>();
    }
}