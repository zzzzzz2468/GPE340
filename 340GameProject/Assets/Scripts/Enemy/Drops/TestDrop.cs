using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrop : MonoBehaviour
{
    DropManager dropManager;

    void Start()
    {
        dropManager = GetComponent<DropManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            Instantiate(dropManager.DropItem());
        }
    }
}