using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Add this script to whatever objects you want to subscribe to projectile events

//Available events to listen to (and the variables in their arguments) are listed in ProjectileEvents
[System.Serializable] public class ProjectileCollisionEvent : UnityEvent<Projectile,ProjectileCollisionArgs>{}
[System.Serializable] public class ProjectileDestructionEvent : UnityEvent<Projectile,ProjectileDestroyedArgs>{}
[System.Serializable] public class ProjectileTargetReachedEvent : UnityEvent<Projectile,ProjectileTargetReachedArgs>{}
[System.Serializable] public class ProjectileTimeoutEvent : UnityEvent<Projectile,ProjectileTimeoutArgs>{}

public class ProjectileListener : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] bool collisionEvents;
    public ProjectileCollisionEvent onProjectileCollision;

    [SerializeField] bool destructionEvents;
    public ProjectileDestructionEvent onProjectileDestroyed;

    [SerializeField] bool targetReachedEvents;
    public ProjectileTargetReachedEvent onProjectileReachedTarget;

    [SerializeField] bool timeoutEvents;
    public ProjectileTimeoutEvent onProjectileTimeout;

    //Subscribe to all ticked events
    void Start(){
      if(collisionEvents)
        ProjectileManager.Instance.ProjectileCollision += ProjectileCollision;

      if(destructionEvents)
        ProjectileManager.Instance.ProjectileDestroyed += ProjectileDestruction;

      if(targetReachedEvents)
        ProjectileManager.Instance.TargetReached += ProjectileTargetReached;

      if(targetReachedEvents)
        ProjectileManager.Instance.ProjectileTimeout += ProjectileTimeout;
    }

    private void ProjectileCollision(Projectile self,ProjectileCollisionArgs args){
      if(args.hitObject == this.gameObject){
        onProjectileCollision.Invoke(self,args);
      }
    }

    private void ProjectileDestruction(Projectile self,ProjectileDestroyedArgs args){
        onProjectileDestroyed.Invoke(self,args);
    }

    private void ProjectileTargetReached(Projectile self,ProjectileTargetReachedArgs args){
        onProjectileReachedTarget.Invoke(self,args);
    }

    private void ProjectileTimeout(Projectile self,ProjectileTimeoutArgs args){
        onProjectileTimeout.Invoke(self,args);
    }
}
