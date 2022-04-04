using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lix.Core;
using TMPro;

public class MenuMoney : MonoBehaviour
{
  [SerializeField] private TMP_Text text;

  private GameManager gameManager;

  // Start is called before the first frame update
  void Start()
  {
    this.gameManager = DIContainer.GetService<GameManager>();
    this.gameManager.OnMoneyChangeEvent += OnMoneyChange;

    OnMoneyChange(this.gameManager.money);
  }

  public void OnMoneyChange(int money)
  {
    text.text = money + " $";
  }

  private void OnDestroy()
  {
    this.gameManager.OnMoneyChangeEvent -= OnMoneyChange;
  }
}
