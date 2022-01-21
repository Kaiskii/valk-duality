using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BulletFireEvent : UnityEvent<Vector2> { }

public class Player : MonoBehaviour {
  [SerializeField]
  float speed = 15f;

  [SerializeField]
  float rotate = 10f;

  [SerializeField]
  float dashSpeed = 10f;
  [SerializeField]
  float dashRange = 2f;

  [SerializeField, Range(0.001f, 0.01f)]
  float afterImageSpacing = 0.001f;

  public BulletFireEvent onShoot;
  public BulletFireEvent onSlash;

  Vector3 lastMoveDir = Vector2.zero;

  void Update() {
    Move();
    Look();
    Dash();
    Fire();
  }

  void Move() {
    lastMoveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    transform.position += new Vector3(
      Input.GetAxis("Horizontal") * speed * Time.deltaTime,
      Input.GetAxis("Vertical") * speed * Time.deltaTime
    );
  }

  void Look() {
    Vector2 mouseDelta = Camera.main.ScreenToViewportPoint(Input.mousePosition) - Camera.main.WorldToViewportPoint(transform.position);

    float angle = Mathf.Atan2(mouseDelta.y, mouseDelta.x)  * Mathf.Rad2Deg;
    Quaternion q = Quaternion.AngleAxis(angle + 180f, Vector3.forward);

    transform.rotation = Quaternion.Slerp(transform.rotation, q, rotate * Time.deltaTime);
  }

  void Dash() {
    if (Input.GetButtonDown("Dash")) {
      StateManager.Instance.TogglePlayerStance();
      StopAllCoroutines();
      StartCoroutine(DashLerp(dashSpeed));
    }
  }

  void Fire() {
    if (Input.GetButtonDown("Fire")) {
      Vector3 mousePos = Input.mousePosition;
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
      worldPosition.z = 0;

      switch(StateManager.Instance.playerWStance) {
        case WeaponStance.SWORD:
          onSlash.Invoke(worldPosition-transform.position);
          break;
        case WeaponStance.GUN:
          onShoot.Invoke(worldPosition-transform.position);
          break;
        default:
          break;
      }
    }
  }

  IEnumerator DashLerp(float lerpSpeed) {
    Vector3 startPos = transform.position;
    Vector3 endPos = transform.position + lastMoveDir * dashRange;
    float time = 0f;

    float wiggle = 0.5f;

    while (
      Mathf.Abs(endPos.x - transform.position.x) > wiggle
      || Mathf.Abs(endPos.y - transform.position.y)  > wiggle
    ) {
      Vector2 res = endPos - transform.position;
      Vector2 absRes = new Vector2(Mathf.Abs(res.x), Mathf.Abs(res.y));
      transform.position = Vector2.Lerp(startPos, endPos, time);

      if (afterImageSpacing < 0) {
        AfterImagePool.Instance.GetFromPool(transform.position, transform.rotation);
        afterImageSpacing = 0.1f;
      }

      afterImageSpacing -= Time.deltaTime * lerpSpeed;
      time += Time.deltaTime * lerpSpeed;
      yield return null;
    }
  }
}
