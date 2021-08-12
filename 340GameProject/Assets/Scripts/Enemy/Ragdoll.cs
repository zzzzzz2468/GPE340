using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ragdoll : MonoBehaviour
{
    private Animator anim;
    private Collider topCollider;
    private Rigidbody topRigidbody;
    private NavMeshAgent agent;

    private List<Collider> ragdollColliders;
    private List<Rigidbody> ragdollRigidBodies;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        topCollider = GetComponent<Collider>();
        //topRigidbody = GetComponent<Rigidbody>();

        ragdollColliders = new List<Collider>(GetComponentsInChildren<Collider>());
        ragdollRigidBodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());

        //ragdollRigidBodies.Remove(topRigidbody);
        ragdollColliders.Remove(topCollider);

        StopRagdoll();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            StartRagdoll();
        if (Input.GetKeyDown(KeyCode.O))
            StopRagdoll();
    }

    public void StartRagdoll()
    {
        anim.enabled = false;
        agent.isStopped = true;
        agent.enabled = false;
        anim.SetFloat("Forward", 0.0f);
        anim.SetFloat("Right", 0.0f);

        foreach (Collider curCol in ragdollColliders)
            curCol.enabled = true;

        foreach (Rigidbody curRB in ragdollRigidBodies)
            curRB.isKinematic = false;

        topCollider.enabled = false;
        //topRigidbody.isKinematic = true;
    }

    public void StopRagdoll()
    {
        anim.enabled = true;
        agent.enabled = true;
        agent.isStopped = false;

        foreach (Collider curCol in ragdollColliders)
            curCol.enabled = false;

        foreach (Rigidbody curRB in ragdollRigidBodies)
            curRB.isKinematic = true;

        topCollider.enabled = true;
        //topRigidbody.isKinematic = false;
    }
}