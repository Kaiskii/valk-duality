using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField]
  float speed = 15.0f;

  [SerializeField]
  float rotate = 10.0f;

  void Update() {
    Move();
    Look();
  }

  void Move() {
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
}
