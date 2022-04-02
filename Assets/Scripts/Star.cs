using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Lix.Core;

[RequireComponent(typeof(Rigidbody2D))]
public class Star : MonoBehaviour
{
  // Settings
  [SerializeField] private float speed = 1.0f;
  [SerializeField] private float maxTorque = 1000.0f;
  [SerializeField] private float torqueDampFactor = 10.0f;

  // Inner Dependencies
  private Rigidbody2D rb;

  // Outer Dependencies
  private World world;
  private IInputListener inputListener;

  // Start is called before the first frame update
  void Start()
  {
    this.rb = this.GetComponent<Rigidbody2D>();

    this.world = DIContainer.GetService<World>();

    inputListener = DIContainer.GetService<IInputListener>();

    inputListener.GetAction(InputActionType.Debug).performed += OnDebugInputPerformed;
  }

  private void OnDebugInputPerformed(InputAction.CallbackContext context)
  {
    InternalDebug.Log("STAR OnDebugInputPerformed");

    Vector2 direction = (world.transform.position - transform.position);

    direction.Normalize();

    // Vector2 direction = (world.transform.position - transform.position);

    // direction.Normalize();

    // float rotateAmount = Vector3.Cross(direction, transform.up).z;

    rb.velocity = direction * 500;
  }

  private void FixedUpdate()
  {
    float distance = Vector3.Distance(world.transform.position, transform.position);

    Vector2 direction = (world.transform.position - transform.position);
    direction.Normalize();

    Vector2 nextDirection = direction;
    nextDirection.Normalize();

    float currentSpeed = Vector3.SqrMagnitude(rb.velocity);
    InternalDebug.Log("STAR currentSpeed: " + currentSpeed);

    float multiply = 1000 / distance;
    if (multiply < 1f) multiply = 1f;

    // rb.AddForce(nextDirection * speed * Time.fixedDeltaTime * multiply);

    TorqueTo(transform.up, nextDirection, rb, maxTorque, torqueDampFactor, 1);

    rb.AddForce(transform.up * speed * Time.fixedDeltaTime);
  }

  /// <summary>
  /// Use AddTorque() to face a specific angle. For 2D physiscs. Should be used in every FixedUpdate() frame.
  /// </summary>
  /// <param name="currentVec"> vector representing the direction we are currently pointing at. (transform.right) </param>
  /// <param name="targetVec"> vector representing the direction we want to point at. </param>
  /// <param name="rb"> Rigidbody to affect. </param>
  /// <param name="maxTorque"> Max torque to apply. </param>
  /// <param name="torqueDampFactor"> Damping factor to avoid undershooting. </param>
  /// <param name="offsetForgive"> Stop applying force when the angles are within this threshold (default 0). </param>
  public static void TorqueTo(Vector3 currentVec, Vector3 targetVec, Rigidbody2D rb, float maxTorque, float torqueDampFactor, float offsetForgive = 0)
  {
    float targetAngle = FindAngle(targetVec);
    float currentAngle = FindAngle(currentVec);
    float angleDifference = AngleDifference(targetAngle, currentAngle);
    if (Mathf.Abs(angleDifference) < offsetForgive) return;

    float torqueToApply = maxTorque * angleDifference / 180f;
    torqueToApply -= rb.angularVelocity * torqueDampFactor;
    rb.AddTorque(torqueToApply, ForceMode2D.Force);
  }

  public static float AngleDifference(float a, float b)
  {
    return ((((a - b) % 360f) + 540f) % 360f) - 180f;
  }

  /// <summary>
  /// Returns the angle (in degrees) in which the vector is pointing.
  /// </summary>
  /// <returns>0-360 angle </returns>
  public static float FindAngle(Vector2 vec)
  {
    return FindAngle(vec.x, vec.y);
  }
  public static float FindAngle(float x, float y)
  {
    float value = (float)((System.Math.Atan2(y, x) / System.Math.PI) * 180);
    if (value < 0) value += 360f;
    return value;
  }

}
