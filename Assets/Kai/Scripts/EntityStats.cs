using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnDeathEvent : UnityEvent { }

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

  [SerializeField]
  OnDeathEvent onDeathEvent;

  protected virtual void Awake() {
    HealthBarManager.Instance.QueueHealthChange(health / maxHealth);
  }

#if UNITY_EDITOR
  protected virtual void Update() {
    if (Input.GetButtonDown("DebugHealthDrop")) {
      DoTakeDamage(10f);
    }
  }
#endif

  void DoTakeDamage(float value) {
    if (health > 0) {
      health = Mathf.Clamp(health + value * -1f, 0f, 100f);
    } else {
      health = 100f;
      onDeathEvent.Invoke();
    }

    HealthBarManager.Instance.QueueHealthChange(health / maxHealth);
  }
}
