using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class DropItem
{
    public GameObject itemToDrop;
    public float dropRate;
}

public class DropManager : MonoBehaviour
{
    public List<DropItem> dropTable = new List<DropItem>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public GameObject DropItem()
    {
        //Creates CDF array
        List<float> CFFArray = new List<float>();

        //fills in cumulative density
        float runningTotal = 0;
        foreach (DropItem item in dropTable)
        {
            runningTotal += item.dropRate;
            CFFArray.Add(runningTotal);
        }

        //Chooses random number and finds drop item
        float randomNumber = Random.Range(0, runningTotal);

        for (int i = 0; i < CFFArray.Count; i++)
        {
            if(randomNumber < CFFArray[i])
                return dropTable[i].itemToDrop;
        }

        Debug.LogError("ERROR: Random number exceeded CDFArray values: DROP MANAGER");
        return null;
    }
}