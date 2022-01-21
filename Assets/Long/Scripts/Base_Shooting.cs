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
  void Update()
  {
      Vector3 mousePos = Input.mousePosition;
      mousePos.z = 0;
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

      if(Input.GetKeyDown(KeyCode.E))
      {
        Shoot(null,worldPosition-transform.position);
      }
  }

  public void Shoot(GameObject target,Vector3 shootDirection)
  {
    Vector3 direction = shootDirection.normalized;
    direction.z = 0;

    Debug.DrawLine(transform.position,direction+transform.position,Color.cyan,0.5f);

    ParticleManager.Instance.CreateParticle(muzzleFlashEffectName,transform.position,direction);
    SoundManager.Instance.Play(fireSFXName);

    if(target == null)
      ProjectileManager.Instance.FireProjectile(projectileID,transform.position,direction);
    else
      ProjectileManager.Instance.FireProjectile(projectileID,transform.position,direction,target,new Vector3(0,0,0));
  }
}
