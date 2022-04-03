using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public int startYear = 2022;
  public int targetScore = 100;
  [SerializeField] private int yearPeriod = 10;
  [SerializeField] private int score;
  [SerializeField] private int targetScoreBase = 60;
  [SerializeField] private int targetScoreAddition = 20;

  // State
  public int currentLevel;

  private float nextActionTime = 0.0f;

  // Events

  public delegate void OnScoreChange(int newValue);

  public event OnScoreChange OnScoreChangeEvent;


  // Start is called before the first frame update
  void Start()
  {
    this.startYear = DateTime.Now.Year;

    StartNextLevel();
  }

  public void StartNextLevel()
  {
    this.currentLevel++;
    this.targetScore = ((currentLevel - 1) * targetScoreAddition) + targetScoreBase;

    OnScoreChangeEvent?.Invoke(score);
    InvokeRepeating("AddScore", 1f, yearPeriod);
  }

  public void StopScore()
  {
    CancelInvoke("AddScore");
  }

  void AddScore()
  {
    this.score++;
    OnScoreChangeEvent?.Invoke(score);
  }
}
