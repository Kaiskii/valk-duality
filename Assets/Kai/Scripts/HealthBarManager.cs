using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour {
  [SerializeField]
  EntityStats playerStats;

  [SerializeField]
  Slider healthSlider;

  float sliderAmount = 0f;

  [SerializeField]
  float lerpRate = 1.0f;
  float lerpTimer = 0f;

  [SerializeField]
  float tolerance = 0.005f;

  [SerializeField]
  AnimationCurve healthCurve;

  public static HealthBarManager Instance;

  Queue<IEnumerator> healthChangeQueue = new Queue<IEnumerator>();

  float totalHealthChange = 0f;

  IEnumerator nextHealthChange;

  Coroutine currentActive;

  private void Awake() {
    Instance = this;
    healthSlider.value = sliderAmount;
  }

  public void QueueHealthChange(float currentHealth) {
    totalHealthChange = currentHealth;
    nextHealthChange = LerpToCurrentHealth(totalHealthChange);

    if (currentActive == null)
      currentActive = StartCoroutine(nextHealthChange);
  }

  public IEnumerator LerpToCurrentHealth(float currentHealth) {
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
