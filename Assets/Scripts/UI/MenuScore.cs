using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Lix.Core;

public class MenuScore : MonoBehaviour
{
  [SerializeField] private Slider slider;
  [SerializeField] private TMP_Text text;

  // Outer Dependencies
  private GameManager gameManager;
  // Start is called before the first frame update
  void Start()
  {
    this.gameManager = DIContainer.GetService<GameManager>();

    this.gameManager.OnScoreChangeEvent += OnScoreChange;

    this.text.text = "Year " + this.gameManager.startYear + " / " + (this.gameManager.startYear + this.gameManager.targetScore);
  }

  private void OnScoreChange(int score)
  {
    this.text.text = "Year " + (this.gameManager.startYear + score) + " / " + (this.gameManager.startYear + this.gameManager.targetScore);

    int prevTarget = (this.gameManager.currentLevel - 1) * 100;

    slider.value = (float)(score - prevTarget) / (float)(this.gameManager.targetScore - prevTarget);
  }

  private void OnDestroy()
  {
    this.gameManager.OnScoreChangeEvent -= OnScoreChange;
  }
}
