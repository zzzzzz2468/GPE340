using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PawnData : MonoBehaviour
{
    ///<Summary>
    ///All the variables that are needed for every pawn, player or ai
    ///</Summary>

    #region Speed Variables
    ///<Summary>
    ///The different speeds needed to allow the player to move or turn
    ///</Summary>

    [Header("Speeds")]

    [Tooltip("Speeds the pawn up when they are on the ground")]
    [SerializeField] protected float speed = 4;

    [Tooltip("The speed the pawn can rotate to a given point")] 
    [SerializeField] protected float turnSpeed = 180;
    #endregion

    #region Non-Accessible Variables
    ///<Summary>
    ///All of the variables that aren't accessible to other scripts
    ///</Summary>

    protected Animator anim; //gets the animatior on the pawn
    #endregion

    #region Awake Function
    ///<Summary>
    ///The awake function sets any of the variables that need to be set at the start of the game
    ///</Summary>

    private void Awake()
    {
        anim = GetComponent<Animator>(); //sets the animator
    }
    #endregion
}