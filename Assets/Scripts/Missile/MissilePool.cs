using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lix.Core;

public class MissilePool : GameObjectPool
{
  protected override void Awake()
  {
    PoolManager.Add(this.GetType().Name, this);
  }

  protected override void OnTakeFromPool(GameObject go)
  {
    if (go == null)
    {
      InternalDebug.LogError("ObstaclePool: Trying to take an item from the pool that is null");
      return;
    }
  }
}
