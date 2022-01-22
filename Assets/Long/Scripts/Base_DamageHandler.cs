using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_DamageHandler : MonoBehaviour
{
  [SerializeField] EntityStats targetStats;
  [SerializeField] bool debug;

  void Start()
  {
    ProjectileManager.Instance.ProjectileCollision += OnProjectileHit;
  }

  public void OnProjectileHit(Projectile proj,ProjectileCollisionArgs hitArgs){
    if(hitArgs.hitObject == this.gameObject){
      //Fetch hit projectilePayload
      SO_Damage damageValue = (SO_Damage)proj.projectileData.payload;
      if (debug) Debug.Log(this.gameObject.name + " was hit by " + proj.projectileID + " for " + damageValue.damage);
      if(targetStats) targetStats.DoTakeDamage(damageValue.damage);
    }
  }
}
