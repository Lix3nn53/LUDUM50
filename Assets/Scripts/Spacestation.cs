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

  // Public Properties
  [SerializeField] private float movementSpeed = 100f;

  // Properties
  private float movementInput;

  // Start is called before the first frame update
  void Start()
  {
    this.world = DIContainer.GetService<World>();

    inputListener = DIContainer.GetService<IInputListener>();

    InputAction moveAction = inputListener.GetAction(InputActionType.Move);
    moveAction.performed += OnMovementInputPerformed;
    moveAction.canceled += OnMovementInputCanceled;
  }

  public void OnMovement(float movement)
  {
    this.movementInput = movement;
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

    transform.RotateAround(world.transform.position, Vector3.forward, Time.fixedDeltaTime * movementSpeed * movementInput);
  }
}
