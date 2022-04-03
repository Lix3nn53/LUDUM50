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
  }

  private void OnScoreChange(int score)
  {
    int targetScoreTotal = 0;

    for (int i = 1; i <= gameManager.currentLevel; i++)
    {
      targetScoreTotal += gameManager.GetTargetScore(i);
    }

    int targetScore = gameManager.GetTargetScore(gameManager.currentLevel);
    int prevTargetScoreTotal = targetScoreTotal - gameManager.GetTargetScore(gameManager.currentLevel);

    int currentYear = this.gameManager.startYear + prevTargetScoreTotal;
    int targetYear = this.gameManager.startYear + targetScoreTotal;

    this.text.text = "Year " + currentYear + " / " + targetYear;

    slider.value = (float)(score) / (float)(targetScore);
  }

  private void OnDestroy()
  {
    this.gameManager.OnScoreChangeEvent -= OnScoreChange;
  }
}
