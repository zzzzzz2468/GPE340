using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : PawnData
{
    //functions needed for all characters, players and ai
    public void RotateTowards(Vector3 lookAtPoint)
    {
        //Find the rotation to look at
        Quaternion goalRotation;
        goalRotation = Quaternion.LookRotation(lookAtPoint - transform.position, Vector3.up);

        //Rotates towards the target
        transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, turnSpeed * Time.deltaTime);
    }
}