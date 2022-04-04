using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Lix.Core;

public class Spacestation : MonoBehaviour
{
  // Dependencies
  private World world;
  private IInputListener inputListener;

  // Properties
  private float movementInput;
  private InputAction aimAction;
  private UpgradeManager upgradeManager;

  // Inspector Properties

  [SerializeField] private float startAngle = -60.0f;

  // Start is called before the first frame update
  void Start()
  {
    this.world = DIContainer.GetService<World>();

    inputListener = DIContainer.GetService<IInputListener>();

    upgradeManager = DIContainer.GetService<UpgradeManager>();

    InputAction moveAction = inputListener.GetAction(InputActionType.Move);
    moveAction.performed += OnMovementInputPerformed;
    moveAction.canceled += OnMovementInputCanceled;

    aimAction = inputListener.GetAction(InputActionType.Aim);

    transform.RotateAround(world.transform.position, Vector3.forward, startAngle);
  }

  public void OnMovement(float movement)
  {
    this.movementInput = movement;
  }

  private void Update()
  {
    Vector2 mouseScreen = aimAction.ReadValue<Vector2>();
    Vector2 worldScreen = Camera.main.WorldToScreenPoint(world.transform.position);
    Vector2 stationScreen = Camera.main.WorldToScreenPoint(this.transform.position);

    Vector2 worldToMouse = -(worldScreen - mouseScreen);
    worldToMouse.Normalize();

    Vector2 worldToStation = -(worldScreen - stationScreen) / 2;
    worldToStation.Normalize();

    // Debug.DrawLine(worldToMouse, worldToStation, Color.red);

    float movement = AngleDir(worldToStation, worldToMouse, Camera.main.transform.forward);

    this.OnMovement(movement);
  }

  float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
  {
    Vector3 perp = Vector3.Cross(fwd, targetDir);
    float dir = Vector3.Dot(perp, up);
    // InternalDebug.Log(dir);

    if (Mathf.Abs(dir) < 0.1f)
    {
      return 0f;
    }

    if (dir > 0f)
    {
      return 1f;
    }
    else if (dir < 0f)
    {
      return -1f;
    }
    else
    {
      return 0f;
    }
  }

  private void OnMovementInputPerformed(InputAction.CallbackContext context)
  {
    InputDevice device = context.control.device;
    float movement = context.ReadValue<float>();

    this.OnMovement(movement);
  }

  private void OnMovementInputCanceled(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      Vector2 movement = context.ReadValue<Vector2>();
      this.OnMovement(movement.x);
    }
    else if (context.canceled)
    {
      this.OnMovement(0f);
    }
  }

  // Update is called once per frame
  private void FixedUpdate()
  {
    if (movementInput == 0)
    {
      return;
    }

    transform.RotateAround(world.transform.position, Vector3.forward, Time.fixedDeltaTime * upgradeManager.GetStationSpeed() * movementInput);
  }

  private void OnDestroy()
  {
    InputAction moveAction = inputListener.GetAction(InputActionType.Move);
    moveAction.performed -= OnMovementInputPerformed;
    moveAction.canceled -= OnMovementInputCanceled;
  }
}
