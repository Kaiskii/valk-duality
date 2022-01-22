using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Projectile", menuName = "ScriptableObjects/Projectile Data", order = 1)]
public class ProjectileDataSO : ScriptableObject
{
  public enum ExecutionType
  {
    ON_TICK,
    ON_TURN
  }

  public enum SpriteMode
  {
    FACE_MOVEMENT_DIRECTION,
    FLIP_X,
    FLIP_Y
  }
  [Header("Layer")]
  public string layerName = "Default";

  [Header("Sprite")]
  public float scale = 1;
  public Sprite sprite;
  public Color spriteColor = Color.white;
  public SpriteMode spriteMode;

  [Header("Effects")]
  public GameObject attachedEffectReference;
  public Vector3 effectOffset;

  [Header("Movement")]
  public ExecutionType executionType;
  public float maxSpeed;
  public float speedCurveTime;
  public AnimationCurve speedCurve;
  
  [Header("Timeout")]
  //Timeout in steps, otherwise timeout in seconds
  public float timeout;

  [Header("Behaviour")]
  public bool tracksTarget = false;
  public float maxTrackingSpeed = 5;
  public float trackingCurveTime = 5;
  public AnimationCurve trackingCurve;
  public float trackingDuration = -1;
  [Space(10)]
  public bool stopsOnTarget;
  public float stopDistance;
  public bool destroyOnTargetReached;
  [Space(10)]
  public bool destroyOnCollision;

  [Header("Additional")]
  public ScriptableObject payload;
}


