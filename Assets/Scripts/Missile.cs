using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lix.Core;

public class Missile : MonoBehaviour
{
  [SerializeField] private float speed = 1.0f;

  private bool isFired;

  // Inner Dependencies
  private Rigidbody2D rb;

  void Start()
  {
    this.rb = this.GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    if (!isFired) return;

    rb.AddForce(transform.up * speed * Time.fixedDeltaTime);
  }

  public void Fire()
  {
    isFired = true;

    transform.parent = null;
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    InternalDebug.Log("Collision with " + collision.gameObject.name);

    if (collision.relativeVelocity.magnitude > 2)
    {

      // audioSource.Play();
    }
  }
}
