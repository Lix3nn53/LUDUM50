using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lix.Core;

public class UpgradeManager : MonoBehaviour
{
  [Header("Start Values")]
  [SerializeField] private float baseStationSpeed = 20f;
  [SerializeField] private float baseMissilePower = 0.1f; // PhysicsMaterial2D.bounciness
  [SerializeField] private float baseReloadDelay = 3f; // In seconds

  [Header("Add On Upgrade")]
  [SerializeField] private float stationSpeedAdd = 10f;
  [SerializeField] private float missilePowerAdd = 0.1f;
  [SerializeField] private float reloadDelayAdd = -0.1f;

  // State
  [SerializeField] private int levelStation = 1;
  public int LevelStation { get { return levelStation; } private set { levelStation = value; } }
  [SerializeField] private int levelMissile = 1;
  public int LevelMissile { get { return levelMissile; } private set { levelMissile = value; } }
  [SerializeField] private int levelReload = 1;
  public int LevelReload { get { return levelReload; } private set { levelReload = value; } }

  // Dependencies

  GameManager gameManager;

  void Start()
  {
    gameManager = DIContainer.GetService<GameManager>();
  }

  public float GetStationSpeed()
  {
    return baseStationSpeed + (levelStation * stationSpeedAdd);
  }

  public float GetMissilePower()
  {
    return baseMissilePower + (levelMissile * missilePowerAdd);
  }

  public float GetReloadDelay()
  {
    return baseReloadDelay + (levelReload * reloadDelayAdd);
  }

  public int GetUpgradeCost(int currentLevel)
  {
    return currentLevel * 2;
  }

  public bool BuyUpgradeStation()
  {
    int money = gameManager.Money;

    int cost = GetUpgradeCost(LevelStation);
    if (money >= cost)
    {
      gameManager.AddMoney(-cost);
      LevelStation++;
      return true;
    }

    return false;
  }

  public bool BuyUpgradeMissile()
  {
    int money = gameManager.Money;

    int cost = GetUpgradeCost(LevelMissile);
    if (money >= cost)
    {
      gameManager.AddMoney(-cost);
      LevelMissile++;
      return true;
    }

    return false;
  }

  public bool BuyUpgradeReload()
  {
    int money = gameManager.Money;

    int cost = GetUpgradeCost(LevelReload);
    if (money >= cost)
    {
      gameManager.AddMoney(-cost);
      LevelReload++;
      return true;
    }

    return false;
  }
}
