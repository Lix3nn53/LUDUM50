using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
  [SerializeField] private float speed = 1f;

  // Update is called once per frame
  void Update()
  {
    transform.RotateAround(transform.position, transform.up, speed * Time.deltaTime);
  }
}
