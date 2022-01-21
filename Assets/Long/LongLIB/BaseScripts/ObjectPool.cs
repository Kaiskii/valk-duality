using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inherit from this base class to create a pool.
/// e.g. public class MyClassName : ObjectPool<MyClassName> {}
/// </summary>

public class ObjectPool : Singleton<ObjectPool>
{
  List<GameObject> pooledObjects;

  public GameObject CreateObject(GameObject newObject)
  {
    //Search pool for object
    for (int i = 0;i<pooledObjects.Count;++i)
    {
      if (pooledObjects[i].GetType() == newObject.GetType() && pooledObjects[i].activeInHierarchy)
      {
        return pooledObjects[i];
      }
    }

    //If not, create a new one
    GameObject newObj = Object.Instantiate(newObject,this.transform);
    pooledObjects.Add(newObj);
    return newObj;
  }
}
