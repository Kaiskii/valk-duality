using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour {
  [SerializeField]
  EntityStats playerStats;

  [SerializeField]
  public Slider healthSlider;

  float sliderAmount = 0f;

  [SerializeField]
  float lerpRate = 1.0f;
  float lerpTimer = 0f;

  [SerializeField]
  float tolerance = 0.005f;

  [SerializeField]
  AnimationCurve healthCurve;

  float totalHealthChange = 0f;

  IEnumerator nextHealthChange;

  Coroutine currentActive;

  public static HealthBarManager Instance;

  private void Awake() {
    Instance = this;
    sliderAmount = 0f;
  }

  private void Start() {
    QueueHealthChange(1.0f);
  }

  public void QueueHealthChange(float currentHealth) {
    totalHealthChange = currentHealth;
    nextHealthChange = LerpToCurrentHealth(totalHealthChange);

    if (currentActive == null)
      currentActive = StartCoroutine(nextHealthChange);
  }

  public IEnumerator LerpToCurrentHealth(float currentHealth) {
    GreyBarManager.Instance.TriggerBarChange(currentHealth, sliderAmount);

    while (healthSlider.value <= currentHealth - tolerance || healthSlider.value >= currentHealth + tolerance) {
      healthSlider.value = Mathf.Lerp(sliderAmount, currentHealth, healthCurve.Evaluate(lerpTimer));
      lerpTimer += lerpRate * Time.deltaTime;

      yield return null;
    }

    sliderAmount = currentHealth;
    lerpTimer = 0f;

    if (nextHealthChange != null) {
      currentActive = StartCoroutine(nextHealthChange);
      nextHealthChange = null;
      totalHealthChange = 0f;
    } else {
      currentActive = null;
    }
  }
}
