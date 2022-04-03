using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLevelComplete : MonoBehaviour
{
  private void Start()
  {

    foreach (Transform child in transform)
    {
      child.gameObject.SetActive(false);
    }
  }
  public void OnLevelComplete()
  {
    foreach (Transform child in transform)
    {
      child.gameObject.SetActive(true);
    }
  }
}
