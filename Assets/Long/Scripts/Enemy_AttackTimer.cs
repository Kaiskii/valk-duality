using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AttackTimer : MonoBehaviour
{
  [SerializeField] float attackSpeed = 3;
  float elapsedTime = 0;

  public BulletFireEvent onAttack;

    // Update is called once per frame
    void Update()
    {
        elapsedTime+=Time.deltaTime;
        if(elapsedTime >= attackSpeed)
        {
          elapsedTime = 0;
          PerformAttack();
        }
    }

    void PerformAttack()
    {
      onAttack.Invoke(Vector2.left);
    }
}
