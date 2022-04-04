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

  private TrailRenderer trailRenderer;

  // Outer Dependencies

  private MissileContainer missileContainer;
  private AudioManager audioManager;

  void Start()
  {
    this.audioManager = DIContainer.GetService<AudioManager>();

    this.rb = this.GetComponent<Rigidbody2D>();
    this.trailRenderer = this.GetComponentInChildren<TrailRenderer>();

    this.missileContainer = DIContainer.GetService<MissileContainer>();
    UpgradeManager upgradeManager = DIContainer.GetService<UpgradeManager>();

    this.rb.sharedMaterial.bounciness = upgradeManager.GetMissilePower();

    this.trailRenderer.gameObject.SetActive(false);
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

    this.trailRenderer.gameObject.SetActive(true);
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (!isFired)
    {
      missileContainer.ReloadCoroutine();
    }

    explosion.transform.parent = null;
    explosion.gameObject.SetActive(true); // Particle system will be destroyed after it has finished playing

    // audioSource.Play();
    audioManager.Play("MissileExplosion");

    Destroy(gameObject);
    // Cant return to pool because explosion is removed from object hierarchy
    // PoolManager.Get("MissilePool").Pool.Release(gameObject);
  }
}
