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

    sr.color = new Color(color.r, color.g, color.b, 0.75f);

    StartCoroutine(DashLerp(6f));
  }

  IEnumerator DashLerp(float lerpSpeed) {
    yield return new WaitForSeconds(0.08f);
    Vector3 startPos = transform.position;
    float time = 0f;

    float wiggle = 0.5f;

    while (
      Mathf.Abs(pTransform.position.x - transform.position.x) > wiggle
      || Mathf.Abs(pTransform.position.y - transform.position.y)  > wiggle
    ) {
      Vector2 res = pTransform.position - transform.position;
      Vector2 absRes = new Vector2(Mathf.Abs(res.x), Mathf.Abs(res.y));
      transform.position = Vector2.Lerp(startPos, pTransform.position, time);

      time += Time.deltaTime * lerpSpeed;
      yield return null;
    }

    AfterImagePool.Instance.AddToPool(gameObject);
    gameObject.SetActive(false);
  }
}
