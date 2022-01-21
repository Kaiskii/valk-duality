using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
  [SerializeField]
  GameObject projectileObject;
  List<GameObject> projectilePool;

  //EVENTS
  public delegate void ProjectileTimeoutHandler(GameObject source,ProjectileTimeoutArgs args);
  public event ProjectileTimeoutHandler ProjectileTimeout;
  public void OnProjectileTimeout(GameObject obj,ProjectileTimeoutArgs args){
    if(ProjectileTimeout != null){
      ProjectileTimeout(obj,args);
    }
  }

  public delegate void TargetReachedHandler(GameObject source,TargetReachedArgs args);
  public event TargetReachedHandler TargetReached;
  public void OnTargetReached(GameObject obj,TargetReachedArgs args){
    if(TargetReached != null){
      TargetReached(obj,args);
    }
  }

  public delegate void ProjectileCollisionHandler(GameObject source,ProjectileCollisionArgs args);
  public event ProjectileCollisionHandler ProjectileCollision;
  public void OnProjectileCollision(GameObject obj,ProjectileCollisionArgs args){
    if(ProjectileCollision != null){
      ProjectileCollision(obj,args);
    }
  }

  public delegate void ProjectileDestroyedHandler(GameObject source,ProjectileDestroyedArgs args);
  public event ProjectileDestroyedHandler ProjectileDestroyed;
  public void OnProjectileDestroyed(GameObject obj,ProjectileDestroyedArgs args){
    if(ProjectileDestroyed != null){
      ProjectileDestroyed(obj,args);
    }
  }

  void Start()
  {
    projectilePool = new List<GameObject>();
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
      if (!projectilePool[i].activeInHierarchy)
      {
        newProjectile = projectilePool[i];
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

      projectilePool.Add(newObj);
      newProjectile = newObj;
    }

    newProjectile.transform.position = position;
    Projectile proj = newProjectile.GetComponent<Projectile>();
    proj.OnCreate(id);
    return newProjectile;
  }
}
