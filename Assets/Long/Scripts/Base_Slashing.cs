using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Slashing : MonoBehaviour
{
  [SerializeField]
  int projectileID;

  [SerializeField]
  string slashEffectName = "Pew";
  [SerializeField]
  string slashSFXName = "GunFX";

  [SerializeField]
  float slashOffset;

  [SerializeField]
  GameObject target;

  public void Slash(Vector2 shootDirection)
  {
    Vector3 direction = shootDirection.normalized;

    Debug.DrawLine(transform.position,direction+transform.position,Color.cyan,0.5f);

    ParticleManager.Instance.CreateParticle(slashEffectName,transform.position,direction,this.transform);
    SoundManager.Instance.Play(slashSFXName);

    ProjectileManager.Instance.FireProjectile(projectileID,transform.position+direction*slashOffset,direction,this.transform);
  }
}
