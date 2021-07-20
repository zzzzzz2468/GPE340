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
        enemySpawned.GetComponent<EnemyController>().spawnPoint = gameObject;
    }

    //is called when the object dies
    public void SpawnedObjectDeath()
    {
        objectSpawned = false;
    }
}
