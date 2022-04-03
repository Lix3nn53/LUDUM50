using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGameOver : MonoBehaviour
{
  private void Start()
  {

    foreach (Transform child in transform)
    {
      child.gameObject.SetActive(false);
    }
  }
  public void OnGameOver()
  {
    foreach (Transform child in transform)
    {
      child.gameObject.SetActive(true);
    }
  }
}
