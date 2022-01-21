using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImagePool : MonoBehaviour {
  [SerializeField]
  GameObject afterImagePref;

  Queue<GameObject> availableObjects = new Queue<GameObject>();

  public static AfterImagePool Instance { get; private set; }

  private void Awake() {
    Instance = this;
  }

  void GrowPool () {
    for (int i = 0; i < 10; i += 1) {
      GameObject newInstance = Instantiate(afterImagePref);
      newInstance.transform.SetParent(transform);
      AddToPool(newInstance);
    }
  }

  public void AddToPool(GameObject instance)
  {
    instance.SetActive(false);
    availableObjects.Enqueue(instance);
  }

  public GameObject GetFromPool(Vector2 spawnPos, Quaternion spawnRot)
  {
    if (availableObjects.Count == 0)
      GrowPool();

    GameObject obj = availableObjects.Dequeue();
    obj.SetActive(true);
    obj.GetComponent<AfterImageSprite>().Reset(spawnPos, spawnRot);

    return obj;
  }

}
