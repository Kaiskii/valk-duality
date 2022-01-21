using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField]
  AnimationCurve movementCurve;

  [SerializeField]
  float speed = 15f;

  [SerializeField]
  float rotate = 10f;

  [SerializeField]
  float dashRange = 2f;

  Vector3 lastMoveDir = Vector2.zero;

  void Update() {
    Dash();
    Move();
    Look();
  }

  void Move() {
    lastMoveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

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
      transform.position += lastMoveDir * dashRange;
    }
  }
}
