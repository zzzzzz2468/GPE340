using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //different variables needed to set up spawning
    [SerializeField] private GameObject objectToSpawn;
    [HideInInspector] public bool objectSpawned = false;
    [SerializeField] private float timeBetweenSpawns = 10f;

    private float timer;

    [Header("Gizmos")]
    [SerializeField] private Color gizmoColor = Color.white;
    [SerializeField] private Vector3 boxSize = new Vector3(1, 2, 1);

    //spawns the target at the start
    private void Start()
    {
        SpawnObject();
    }

    //checks if the target is spawned, if not, spawn one after a set time
    private void Update()
    {
        if (!objectSpawned)
        {
            if (timer > timeBetweenSpawns)
            {
                timer = 0;
                SpawnObject();
            }
            else if (timer < timeBetweenSpawns)
            {
                timer += Time.deltaTime;
            }
        }
    }

    //instatiates the object at the spawnpoint, and sets the spawnpoint in the spawned object
    public void SpawnObject()
    {
        objectSpawned = true;
        var enemySpawned = Instantiate(objectToSpawn, transform.position, transform.rotation);
        enemySpawned.GetComponent<Health>().spawnPoint = gameObject;
    }

    //is called when the object dies
    public void SpawnedObjectDeath()
    {
        objectSpawned = false;
    }

    //creates a box around the spawnpoint and an arrow to where the enemy will be facing
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        float boxOffsetY = boxSize.y / 2;
        Gizmos.DrawCube(transform.position + (boxOffsetY * Vector3.up), boxSize);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position + (boxOffsetY * Vector3.up), -transform.right);
    }
}