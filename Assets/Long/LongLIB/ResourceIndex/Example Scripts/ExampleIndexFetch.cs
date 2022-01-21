using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
An example utilization for the Resource Index.
*/

public class ExampleIndexFetch : MonoBehaviour
{
    void Start()
    {
        //GET ALL ASSETS
        List<ExampleResourceSO> allResources = new List<ExampleResourceSO>();
        allResources.AddRange(ResourceIndex.GetAllAssets<ExampleResourceSO>());

        Debug.Log("Found " + allResources.Count + " resources!");
        foreach (ExampleResourceSO resource in allResources)
        {
            Debug.Log(resource.data);
        }

        Debug.Log("Getting resource by ID!");
        //GET ASSET BY ID
        var resourceByID = ResourceIndex.GetAsset<ExampleResourceSO>(0);
        if(resourceByID) Debug.Log(resourceByID.data);
    }
}
