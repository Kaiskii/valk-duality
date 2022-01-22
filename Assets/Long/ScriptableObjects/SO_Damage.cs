using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New DamageType", menuName = "ScriptableObjects/Damage Type", order = 1)]
public class SO_Damage : ScriptableObject
{
  public int damage = 1;
}
