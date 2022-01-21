using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Available events to listen to are listed in ProjectileEvents
public class ExampleListenerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
      ProjectileManager.Instance.ProjectileCollision += ProjectileCollision;


    }

    private void ProjectileCollision(GameObject obj,ProjectileCollisionArgs args){
      if(args.hitObject == this.gameObject){
        Debug.Log(gameObject.name+" hit by " + args.projectileID);
      }
    }
}
