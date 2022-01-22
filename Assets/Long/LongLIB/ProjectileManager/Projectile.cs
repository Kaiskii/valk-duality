using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  SpriteRenderer rend;
  Collider2D col;

  GameObject attachedEffects;

  public int projectileID;
  public ProjectileDataSO projectileData;

  [SerializeField] bool activateOnLoad;
  [SerializeField] GameObject target;
  [SerializeField] Vector3 targetOffset;

  [Header("Internal Variables")]
  [SerializeField] Transform effectHolder;

  bool targetReached;
  
  Vector3 targetPosition;

  Vector3 currentDirection;
  Vector3 targetDirection;

  Vector3 currentRotation;
  float startTime;

  // Start is called before the first frame update
  void Start(){
    OnCreate(projectileID);
  }

  public void OnCreate(int id)
  {
    projectileID = id;
    projectileData = ResourceIndex.GetAsset<ProjectileDataSO>(projectileID);

    if(!projectileData){
      Debug.LogWarning("Trying to fire non-existent projectile with ID:"+projectileID+"!");
      return;
    }

    CreateSprite();
    CreateParticleSystem();
    CreateCollider();

    startTime = Time.time;

    //If we're spawned in with an existing offset or target, just set our target
    if(activateOnLoad){
      if(target){
        SetTarget(Vector3.right,target,targetOffset);
      }else{
        SetTarget(Vector3.right);
      }
    }

    gameObject.SetActive(true);
  }

  void CreateSprite()
  {
    rend = effectHolder.GetComponentInChildren<SpriteRenderer>();

    rend.sprite = projectileData.sprite;
    rend.color = projectileData.spriteColor;
    rend.transform.localScale = new Vector2(projectileData.scale,projectileData.scale);
  }

  void CreateParticleSystem()
  {
    if(projectileData.attachedEffectReference)
    {
      if(attachedEffects) GameObject.Destroy(attachedEffects);
      attachedEffects = Instantiate(projectileData.attachedEffectReference,effectHolder.transform.position,effectHolder.transform.rotation);
      attachedEffects.transform.SetParent(effectHolder);
      attachedEffects.transform.localPosition = projectileData.effectOffset;
    }
  }

  void CreateCollider()
  {
    //TODO: Maybe add an enum that lets you choose which collider in the future

    col = GetComponentInChildren<Collider2D>();
    //Set collider bounds depending what collider it is
    if(col.GetType() == typeof(CircleCollider2D))
    {
      CircleCollider2D thisCollider = GetComponentInChildren<CircleCollider2D>();
      thisCollider.radius = projectileData.scale/2f;
    }
    //Set collider layer
    if(LayerMask.NameToLayer(projectileData.layerName) != -1){
      col.gameObject.layer = LayerMask.NameToLayer(projectileData.layerName);
    }else{
      Debug.LogWarning(name + " has invalid LayerName '"+ projectileData.layerName +"'; using Default instead!");
    }
  }

  public void SetTarget(Vector3 startDirection, GameObject newTarget, Vector3 offset)
  {
    target = newTarget;
    targetOffset = offset;

    targetPosition = target.transform.position+targetOffset;
    targetDirection = Vector3.Normalize(targetPosition-transform.position);
    currentDirection = startDirection;

    gameObject.SetActive(true);
  }

  public void SetTarget(Vector3 startDirection)
  {
    target = null;
    targetOffset = startDirection;

    targetPosition = transform.position+targetOffset;
    targetDirection = Vector3.Normalize(targetPosition-transform.position);
    currentDirection = startDirection;

    gameObject.SetActive(true);
  }


  public void OnDestroy()
  {
    ProjectileManager.Instance?.OnProjectileDestroyed(this,new ProjectileDestroyedArgs(){projectileID = this.projectileID});
    gameObject.SetActive(false);
  }

  // Update is called once per frame
  void Update(){
    UpdateTargetPosition();
    UpdateMovement();
    UpdateRotation();
    UpdateTimeout();
  }

  void UpdateMovement()
  {
    if(projectileData.executionType != ProjectileDataSO.ExecutionType.ON_TICK) return;
    if(currentDirection.magnitude <= 0) return;

    //Scale speed according to curve time
    float currentSpeed = EvaluateCurve(projectileData.maxSpeed,projectileData.speedCurve,projectileData.speedCurveTime);

    //Move towards target
    if(!(projectileData.stopsOnTarget && Vector3.Distance(transform.position,targetPosition) <= projectileData.stopDistance+0.05f)){
      transform.Translate(currentDirection*Time.deltaTime*currentSpeed);
    } else if (!targetReached){
      targetReached = true;
      ProjectileManager.Instance?.OnTargetReached(this,new ProjectileTargetReachedArgs(){projectileID = this.projectileID});
      if(projectileData.destroyOnTargetReached) {
        OnDestroy();
      }
    }
  }

  void UpdateTargetPosition(){
    //If the projectile tracks, update the target position
    if(projectileData.tracksTarget && target){
      targetPosition = target.transform.position+targetOffset;
      targetDirection = Vector3.Normalize(targetPosition-transform.position);
    }

    //The step size is equal to evaluated tracking speed times frame time.
    if(projectileData.trackingDuration <= 0 || (Time.time-startTime) < projectileData.trackingDuration ){
      float singleStep = EvaluateCurve(projectileData.maxTrackingSpeed,projectileData.trackingCurve,projectileData.trackingCurveTime) * Time.deltaTime;
      currentDirection = Vector3.RotateTowards(currentDirection, targetDirection, singleStep, 0.0f);
    }
    
    //Debug directions
    Debug.DrawRay(transform.position, currentDirection, Color.green);
    Debug.DrawRay(transform.position, targetDirection, Color.red);
  }

  void UpdateRotation(){
    switch(projectileData.spriteMode)
    {
      case ProjectileDataSO.SpriteMode.FACE_MOVEMENT_DIRECTION:
        float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
        effectHolder.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
        break;
      case ProjectileDataSO.SpriteMode.FLIP_X:
        rend.flipX = (currentDirection.x+transform.position.x < transform.position.x);
        break;
      case ProjectileDataSO.SpriteMode.FLIP_Y:
        rend.flipY = (currentDirection.y+transform.position.y < transform.position.y);
        break;
    }
  }

  void UpdateTimeout(){
    //Timeout
    if(projectileData.timeout > 0 && (Time.time-startTime) >= projectileData.timeout)
    {
      ProjectileManager.Instance?.OnProjectileTimeout(this,new ProjectileTimeoutArgs(){projectileID = this.projectileID});
      OnDestroy();
    }
  }

  // Call when collider hits object
  void OnTriggerEnter2D(Collider2D col)
  {
      ProjectileManager.Instance?.OnProjectileCollision(this,new ProjectileCollisionArgs(){hitObject = col.gameObject});
      if(projectileData.destroyOnCollision) OnDestroy();
  }

  float EvaluateCurve(float maxValue, AnimationCurve curve, float curveTime)
  {
    if(curveTime <= 0)
      return maxValue;
    else
      return maxValue*curve.Evaluate((Time.time-startTime)/curveTime);
  }
}


