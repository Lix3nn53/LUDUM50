using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lix.Core;

public class Missile : MonoBehaviour
{
  [SerializeField] private ParticleSystem explosion;
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
    InternalDebug.Log("Missile OnCollisionEnter2D collider: " + collision.collider.name, collision.collider);
    InternalDebug.Log("Missile OnCollisionEnter2D otherCollider: " + collision.otherCollider.name, collision.otherCollider);
    explosion.transform.parent = null;
    explosion.transform.gameObject.SetActive(true);
    explosion.Play(); // Particle system will be destroyed after it has finished playing

    // audioSource.Play();

    Destroy(gameObject);
    // Cant return to pool because explosion is removed from object hierarchy
    // PoolManager.Get("MissilePool").Pool.Release(gameObject);
  }
}
