using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnDeathEvent : UnityEvent { }
[System.Serializable]
public class OnDamageEvent : UnityEvent { }

public class EntityStats : MonoBehaviour {
  public float health = 100f;
  public float maxHealth = 100f;
  [SerializeField] bool affectsHeathUI = false;

  [SerializeField]
  protected float speed = 13f;
  [SerializeField]
  protected float rotate = 10f;

  [SerializeField]
  protected float dashSpeed = 12f;
  [SerializeField]
  protected float dashRange = 4f;
  
  [Header("SFX")]
  [SerializeField] string hurtSFX = "";
  [SerializeField] string deathSFX = "";

  [Header("Events")]
  [SerializeField]
  OnDeathEvent onDeathEvent;
  [SerializeField]
  OnDamageEvent onDamageEvent;

  protected virtual void Awake() {
    if(affectsHeathUI) HealthBarManager.Instance.QueueHealthChange(health / maxHealth);
  }

#if UNITY_EDITOR
  protected virtual void Update() {
    if (Input.GetButtonDown("DebugHealthDrop")) {
      DoTakeDamage(10f);
    }
  }
#endif

  public void DoTakeDamage(float value) {
    if (health > 0) {
      health = Mathf.Clamp(health + value * -1f, 0f, 100f);
      SoundManager.Instance.Play(hurtSFX);
    } else {
      health = 100f;
      SoundManager.Instance.Play(deathSFX);
      onDeathEvent.Invoke();
    }

    if(affectsHeathUI) HealthBarManager.Instance.QueueHealthChange(health / maxHealth);
  }
}
