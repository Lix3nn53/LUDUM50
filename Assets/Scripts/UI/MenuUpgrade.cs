using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lix.Core;
using TMPro;

public class MenuUpgrade : MonoBehaviour
{
  // Outer Dependencies

  GameManager gameManager;

  UpgradeManager upgradeManager;

  // Inner Dependencies
  [SerializeField] private TMP_Text balanceText;
  [SerializeField] private TMP_Text costStationText;
  [SerializeField] private TMP_Text costMissileText;
  [SerializeField] private TMP_Text costReloadText;
  [SerializeField] private TMP_Text levelStationText;
  [SerializeField] private TMP_Text levelMissileText;
  [SerializeField] private TMP_Text levelReloadText;

  void Start()
  {
    gameManager = DIContainer.GetService<GameManager>();
    upgradeManager = DIContainer.GetService<UpgradeManager>();

    balanceText.text = "Balance: " + gameManager.Money + "M $";
  }

  public void ButtonUpgradeStation()
  {
    bool bought = upgradeManager.BuyUpgradeStation();
    if (bought)
    {
      balanceText.text = "Balance: " + gameManager.Money + " $";
      levelStationText.text = "Lv " + upgradeManager.LevelStation;
      costStationText.text = "Cost: " + upgradeManager.GetUpgradeCost(upgradeManager.LevelStation) + " $";
    }
  }

  public void ButtonUpgradeMissile()
  {
    bool bought = upgradeManager.BuyUpgradeMissile();
    if (bought)
    {
      balanceText.text = "Balance: " + gameManager.Money + " $";
      levelMissileText.text = "Lv " + upgradeManager.LevelMissile;
      costMissileText.text = "Cost: " + upgradeManager.GetUpgradeCost(upgradeManager.LevelMissile) + " $";
    }
  }

  public void ButtonUpgradeReload()
  {
    bool bought = upgradeManager.BuyUpgradeReload();
    if (bought)
    {
      balanceText.text = "Balance: " + gameManager.Money + " $";
      levelReloadText.text = "Lv " + upgradeManager.LevelReload;
      costReloadText.text = "Cost: " + upgradeManager.GetUpgradeCost(upgradeManager.LevelReload) + " $";
    }
  }
}
