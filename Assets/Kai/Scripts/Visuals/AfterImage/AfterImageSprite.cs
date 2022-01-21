using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageSprite : MonoBehaviour {
  SpriteRenderer sr;

  public Vector3 playerPos;
  public Quaternion playerRot;

  [SerializeField]
  float lifetime = 3.0f;
  float currLifetime = 3.0f;

  [SerializeField]
  float decayRate = 0.5f;

  [SerializeField]
  AnimationCurve alphaCurve = new AnimationCurve();

  [SerializeField]
  Color color;

  private void Awake() {
    sr = this.GetComponent<SpriteRenderer>();
  }

  public void Reset(Vector2 spawnPos, Quaternion spawnRot) {
    currLifetime = lifetime;
    transform.position = spawnPos;
    transform.rotation = spawnRot;
  }

  void Update() {
    currLifetime -= decayRate * Time.deltaTime;

    sr.color = new Color(color.r, color.g, color.b, alphaCurve.Evaluate(currLifetime / lifetime));

    if (currLifetime <= 0f) {
      AfterImagePool.Instance.AddToPool(gameObject);
      gameObject.SetActive(false);
    }
  }
}
