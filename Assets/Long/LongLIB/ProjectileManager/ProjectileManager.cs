using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
  [SerializeField]
  GameObject projectileObject;
  List<Projectile> projectilePool;

  //EVENTS
  public delegate void ProjectileTimeoutHandler(Projectile source,ProjectileTimeoutArgs args);
  public event ProjectileTimeoutHandler ProjectileTimeout;
  public void OnProjectileTimeout(Projectile self,ProjectileTimeoutArgs args){
    if(ProjectileTimeout != null){
      ProjectileTimeout(self,args);
    }
  }

  public delegate void TargetReachedHandler(Projectile source,ProjectileTargetReachedArgs args);
  public event TargetReachedHandler TargetReached;
  public void OnTargetReached(Projectile self,ProjectileTargetReachedArgs args){
    if(TargetReached != null){
      TargetReached(self,args);
    }
  }

  public delegate void ProjectileCollisionHandler(Projectile source,ProjectileCollisionArgs args);
  public event ProjectileCollisionHandler ProjectileCollision;
  public void OnProjectileCollision(Projectile self,ProjectileCollisionArgs args){
    if(ProjectileCollision != null){
      //Debug.Log("ID:"+ self.projectileID + " hit " + args.hitObject.name);
      ProjectileCollision(self,args);
    }
  }

  public delegate void ProjectileDestroyedHandler(Projectile source,ProjectileDestroyedArgs args);
  public event ProjectileDestroyedHandler ProjectileDestroyed;
  public void OnProjectileDestroyed(Projectile self,ProjectileDestroyedArgs args){
    if(ProjectileDestroyed != null){
      ProjectileDestroyed(self,args);
    }
  }

  void Start()
  {
    projectilePool = new List<Projectile>();
  }

  public void FireProjectile(int id,Vector3 position,Vector3 direction, GameObject target,Vector3 offset,Transform parent = null)
  {
    GameObject newProjectile;
    newProjectile = CreateProjectile(id,position,parent);
    newProjectile.GetComponent<Projectile>().SetTarget(direction,target,offset);
  }
  public void FireProjectile(int id,Vector3 position, Vector3 direction,Transform parent = null)
  {
    GameObject newProjectile;
    newProjectile = CreateProjectile(id,position,parent);
    newProjectile.GetComponent<Projectile>().SetTarget(direction);
  }

  GameObject CreateProjectile(int id,Vector3 position, Transform parent = null)
  {
    GameObject newProjectile = null;
    //Search pool for object
    for (int i = 0;i<projectilePool.Count;++i)
    {
      if (!projectilePool[i].gameObject.activeInHierarchy && projectilePool[i].projectileID == id)
      {
        newProjectile = projectilePool[i].gameObject;
      }
    }

    //If not, create a new one
    if(!newProjectile)
    {
      GameObject newObj;

      //If a parent is specified, parent it under them instead
      if(!parent)
        newObj = Instantiate(projectileObject,this.transform);
      else
        newObj = Instantiate(projectileObject,parent);

      projectilePool.Add(newObj.GetComponent<Projectile>());
      newProjectile = newObj;
    }

    newProjectile.transform.position = position;
    Projectile proj = newProjectile.GetComponent<Projectile>();
    proj.OnCreate(id);
    return newProjectile;
  }
}
