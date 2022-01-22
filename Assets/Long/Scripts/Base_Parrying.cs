using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Parrying : MonoBehaviour
{
  [SerializeField]
  int spawnedProjectileID = 0;

  [SerializeField]
  List<int> parryableProjectileIDs = new List<int>();

  [SerializeField]
  string parryEffect = "";
  [SerializeField]
  string normalParrySFX = "";
  [SerializeField]
  string perfectParrySFX = "";

  void Start()
  {
    ProjectileManager.Instance.ProjectileCollision += CheckParryable;
  }

  void CheckParryable(Projectile self,ProjectileCollisionArgs args)
  {
    //Debug.Log("Self:" + self.projectileID + " - " + args.hitObject.name);

    if(self.projectileID == 1)
    {
      Vector3 mousePos = Input.mousePosition;
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
      worldPosition.z = 0;

      Parry(args.hitObject.transform,worldPosition - args.hitObject.transform.position);
    }
      
  }

  void Parry(Transform parriedObject, Vector2 dir)
  {
    Vector2 direction = dir.normalized;

    Debug.DrawLine(transform.position,dir+(Vector2)transform.position,Color.cyan,0.5f);

    ParticleManager.Instance.CreateParticle(parryEffect,transform.position,direction,this.transform);
    SoundManager.Instance.Play(normalParrySFX, 0.5f);

    ProjectileManager.Instance.FireProjectile(spawnedProjectileID,parriedObject.position,direction);
  }
}
