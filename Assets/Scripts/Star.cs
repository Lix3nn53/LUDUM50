using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lix.Core;

[RequireComponent(typeof(Rigidbody2D))]
public class Star : MonoBehaviour
{
  // Settings
  [SerializeField] private float speed = 1.0f;
  [SerializeField] private float rotateSpeed = 10.0f;

  // Inner Dependencies
  private Rigidbody2D rb;

  // Outer Dependencies
  private World world;

  // Start is called before the first frame update
  void Start()
  {
    this.rb = this.GetComponent<Rigidbody2D>();

    this.world = DIContainer.GetService<World>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void FixedUpdate()
  {
    Vector2 direction = (world.transform.position - transform.position);

    direction.Normalize();

    float rotateAmount = Vector3.Cross(direction, transform.up).z;

    rb.angularVelocity = -rotateAmount * rotateSpeed;

    rb.velocity = transform.up * speed;
  }
}
