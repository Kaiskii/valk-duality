using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreyBarManager : MonoBehaviour {
  [SerializeField]
  Slider slider;

  [SerializeField]
  float delay = 0.25f;

  float currentValue = 0f;

  [SerializeField]
  float tolerance = 0.005f;

  [SerializeField]
  float lerpRate = 2f;
  float lerpTimer = 0f;

  [SerializeField]
  AnimationCurve ac;

  Coroutine cr;

  public static GreyBarManager Instance;

  private void Awake() {
    Instance = this;
  }

  private void Start() {
    currentValue = HealthBarManager.Instance.healthSlider.value;
    slider.value = currentValue;
  }

  public void TriggerBarChange(float currentHealth, float startingHealth) {
    if (cr != null) {
      StopAllCoroutines();
      currentValue = startingHealth;
      slider.value = currentValue;
      cr = null;
      lerpTimer = 0f;
    }

    cr = StartCoroutine(LerpToCurrentHealth(currentHealth));
  }

  public IEnumerator LerpToCurrentHealth(float currentHealth) {
    yield return new WaitForSeconds(delay);
    while (slider.value <= currentHealth - tolerance || slider.value >= currentHealth + tolerance) {
      slider.value = Mathf.Lerp(currentValue, currentHealth, ac.Evaluate(lerpTimer));
      lerpTimer += lerpRate * Time.deltaTime;

      yield return null;
    }

    currentValue = currentHealth;
    lerpTimer = 0f;
  }
}
