using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerData : Pawn
{
    ///<Summary>
    ///All of the variables that are player specific, variables that other pawns won't need
    ///</Summary>

    #region Camera Variables
    ///<Summary>
    ///The camera that will allow the camera to follow the player so they can see what they are doing
    ///</Summary>

    [HideInInspector] public Camera playerCam;
    #endregion

    #region Jumping / Falling Variables
    ///<Summary>
    ///All the variables needed to control the falling and jumping movement
    ///</Summary>

    [Header("Jumping / Falling")]

    [Tooltip("The force applied upwards when the player jumps")]
    [SerializeField] protected float jumpheight = 3f;
    
    [Tooltip("The force that pulls the player down when they are in the air")] 
    [SerializeField] protected float gravity = 20f;
    
    [Tooltip("The amount the player can move in the air")] 
    [SerializeField] protected float airControl = 2.5f;
    
    [Tooltip("The amount the jump is smoothed")] 
    [SerializeField] protected float jumpDamp = 2.5f;
    #endregion

    #region Movement / Pushing Variables
    ///<Summary>
    ///All the variables needed control the movement and the pushing that are player specific
    ///</Summary>

    [Header("Movement / Pushing")]

    [Tooltip("The amount the player can step down (stairs)")] 
    [SerializeField] protected float stepDown = 0.1f;
    
    [Tooltip("Speeds the player up on the ground")] 
    [SerializeField] protected float groundSpeed = 1f;
    
    [Tooltip("Amount of force applied to objects with rigidbodies")] 
    [SerializeField] protected float pushPower = 2.0F;
    #endregion

    #region Non-Accessible Variables
    ///<Summary>
    ///All of the variables that are not accessible from other scripts
    ///</Summary>

    protected Vector3 rootMotion; //Adds all the root motion from the anim to use later
    protected Vector3 velocity; //Used to get and set the velocity of the player
    protected bool isJumping; //Checks if the player is jumping or not
    protected CharacterController charCont; //Gets the character controller to access it
    #endregion

    //weapon variables needed
    public Weapon weapon;
    [SerializeField] protected List<GameObject> weapons = new List<GameObject>();
    [SerializeField] protected GameObject rifleContainer;
    protected bool weaponEquipped = false;

    #region Start Function
    ///<Summary>
    ///Start function sets any of the varaibles that need to be set at the start of the game
    ///</Summary>

    private void Start()
    {
        charCont = GetComponent<CharacterController>(); //Sets the character controller
    }
    #endregion
}