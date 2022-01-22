using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour {
  public float health = 100f;
  public float maxHealth = 100f;

  [SerializeField]
  protected float speed = 13f;
  [SerializeField]
  protected float rotate = 10f;

  [SerializeField]
  protected float dashSpeed = 12f;
  [SerializeField]
  protected float dashRange = 4f;

  protected virtual void Awake() {
    HealthBarManager.Instance.QueueHealthChange(health / maxHealth);
  }

#if UNITY_EDITOR
  protected virtual void Update() {
    if (Input.GetButtonDown("DebugHealthDrop")) {
      DoTakeDamage(-10f);
      HealthBarManager.Instance.QueueHealthChange(health / maxHealth);
    }
  }
#endif

  void DoTakeDamage(float value) {
    if (health > 0) {
      health = Mathf.Clamp(health + value, 0f, 100f);
    } else {
      health = 100f;
    }
  }
}
