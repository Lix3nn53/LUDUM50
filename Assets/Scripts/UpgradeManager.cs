using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
  [Header("Start Values")]
  [SerializeField] private float stationSpeed = 50f;
  public float StationSpeed { get { return stationSpeed; } private set { stationSpeed = value; } }
  [SerializeField] private float missilePower = 0.2f; // PhysicsMaterial2D.bounciness
  public float MissilePower { get { return missilePower; } private set { missilePower = value; } }
  [SerializeField] private float reloadDelay = 2f; // In seconds
  public float ReloadDelay { get { return reloadDelay; } private set { reloadDelay = value; } }

  [Header("Add On Upgrade")]
  [SerializeField] private float stationSpeedAdd = 10f;
  [SerializeField] private float missilePowerAdd = 0.1f;
  [SerializeField] private float reloadDelayAdd = -0.1f;

  public void UpgradeStationSpeed()
  {
    stationSpeed += stationSpeedAdd;
  }

  public void UpgradeMissilePower()
  {
    missilePower += missilePowerAdd;
  }

  public void UpgradeReloadDelay()
  {
    reloadDelay += reloadDelayAdd;
  }
}
