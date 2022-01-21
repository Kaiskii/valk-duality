using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponStance {
  SWORD = 0,
  GUN,
};

public class StateManager : MonoBehaviour {
  public static StateManager Instance;

  public WeaponStance playerWStance { get; set; }

  private void Awake() {
    Instance = this;
  }

  public void TogglePlayerStance() {
    playerWStance = (WeaponStance) System.Convert.ToUInt16(!System.Convert.ToBoolean(playerWStance));
  }
}
