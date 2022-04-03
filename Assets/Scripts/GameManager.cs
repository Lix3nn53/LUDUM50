using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lix.Core;

public class GameManager : MonoBehaviour
{
  public int startYear = 2022;
  [SerializeField] private int score;
  [SerializeField] private int yearPeriod = 10;
  [SerializeField] private int targetScoreBase = 60;
  [SerializeField] private int targetScoreAddition = 20;

  // State
  public int currentLevel = 1;

  // Events

  public delegate void OnScoreChange(int newValue);
  public event OnScoreChange OnScoreChangeEvent;

  public delegate void OnGameOver(int year);
  public event OnGameOver OnGameOverEvent;

  public delegate void OnLevelComplete(int year);
  public event OnLevelComplete OnLevelCompleteEvent;

  void Start()
  {
    this.startYear = DateTime.Now.Year;

    StartScore();
    SceneManager.sceneLoaded += OnSceneLoaded;
  }


  // Start is called before the first frame update
  void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    if (scene.buildIndex == 1) // Only scene where score is updated
    {
      StartScore();
    }
  }

  public void StartScore()
  {
    InternalDebug.Log("StartLevel");
    this.score = 0;

    InvokeRepeating("AddScore", 0f, yearPeriod);
  }

  public void StopScore()
  {
    CancelInvoke("AddScore");
  }

  void AddScore()
  {
    this.score++;
    OnScoreChangeEvent?.Invoke(score);

    int targetScore = GetTargetScore(currentLevel);

    if (score >= targetScore)
    {
      Time.timeScale = 0;
      OnLevelCompleteEvent?.Invoke(this.startYear + this.score);
      StopScore();
      this.currentLevel++;
    }
  }

  public void GameOver()
  {
    StopScore();
    OnGameOverEvent?.Invoke(this.startYear + this.score);
    this.currentLevel = 1;
  }

  public int GetTargetScore(int level)
  {
    if (level <= 0)
    {
      return 0;
    }

    return ((level - 1) * targetScoreAddition) + targetScoreBase;
  }
}
