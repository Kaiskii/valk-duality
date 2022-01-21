using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Shooting : MonoBehaviour
{
  [SerializeField]
  int projectileID;

  [SerializeField]
  string muzzleFlashEffectName = "Pew";
  [SerializeField]
  string fireSFXName = "GunFX";

  [SerializeField]
  GameObject target;

  // Update is called once per frame
  /*
  void Update()
  {
      Vector3 mousePos = Input.mousePosition;
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
      worldPosition.z = 0;

      if(Input.GetKeyDown(KeyCode.E))
      {
        Shoot(worldPosition-transform.position);
      }
  }
  */

  public void Shoot(Vector2 shootDirection)
  {
    Vector3 direction = shootDirection.normalized;

    Debug.DrawLine(transform.position,direction+transform.position,Color.cyan,0.5f);

    ParticleManager.Instance.CreateParticle(muzzleFlashEffectName,transform.position,direction);
    SoundManager.Instance.Play(fireSFXName);

    ProjectileManager.Instance.FireProjectile(projectileID,transform.position,direction);
  }
}