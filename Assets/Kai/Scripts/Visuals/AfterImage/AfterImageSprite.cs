using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageSprite : MonoBehaviour {
  public Transform pTransform;

  SpriteRenderer sr;

  public Vector3 playerPos;
  public Quaternion playerRot;
  public Vector3 finalPos;

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

  public void Reset(Vector2 spawnPos, Vector2 endPos, Quaternion spawnRot) {
    currLifetime = lifetime;
    transform.position = spawnPos;
    transform.rotation = spawnRot;
    finalPos = endPos;

    StartCoroutine(DashLerp(6f));
  }

  void Update() {
    // currLifetime -= decayRate * Time.deltaTime;
    // sr.color = new Color(color.r, color.g, color.b, alphaCurve.Evaluate(currLifetime / lifetime));
  }

  IEnumerator DashLerp(float lerpSpeed) {
    Vector3 startPos = transform.position;
    float time = 0f;

    float wiggle = 0.5f;

    while (
      Mathf.Abs(finalPos.x - transform.position.x) > wiggle
      || Mathf.Abs(finalPos.y - transform.position.y)  > wiggle
    ) {
      Vector2 res = finalPos - transform.position;
      Vector2 absRes = new Vector2(Mathf.Abs(res.x), Mathf.Abs(res.y));
      transform.position = Vector2.Lerp(startPos, finalPos, time);

      time += Time.deltaTime * lerpSpeed;
      yield return null;
    }

    AfterImagePool.Instance.AddToPool(gameObject);
    gameObject.SetActive(false);
  }
}
