using System;
using UnityEngine;

//ProjectileManager.Instance.ProjectileDestroyed += [Function];
public class ProjectileDestroyedArgs:EventArgs{
  public int projectileID;
}

//ProjectileManager.Instance.ProjectileTimeout += [Function];
public class ProjectileTimeoutArgs : EventArgs{
  public int projectileID;
}

//ProjectileManager.Instance.TargetReached += [Function];
public class ProjectileTargetReachedArgs : EventArgs{
  public int projectileID;
  public GameObject reachedTarget;
}

//ProjectileManager.Instance.OnCollsion += [Function];
public class ProjectileCollisionArgs : EventArgs{
  public GameObject hitObject;
}