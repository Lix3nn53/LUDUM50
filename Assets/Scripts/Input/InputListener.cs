using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Lix.Core;

[RequireComponent(typeof(PlayerInput))]
public class InputListener : MonoBehaviour, IInputListener
{

  private PlayerInput playerInput;
  private InputAction ActionMove;
  private InputAction ActionPause;
  private InputAction ActionFire;

  protected void Awake()
  {
    if (playerInput != null) return;

    playerInput = GetComponent<PlayerInput>();

    if (playerInput == null) return;
    if (playerInput.currentActionMap == null) return;

    ActionMove = playerInput.currentActionMap.FindAction("Move");

    ActionPause = playerInput.currentActionMap.FindAction("Pause");

    ActionFire = playerInput.currentActionMap.FindAction("Fire");
  }

  public InputAction GetAction(InputActionType type)
  {
    switch (type)
    {
      case InputActionType.Move:
        return ActionMove;
      case InputActionType.Pause:
        return ActionPause;
      case InputActionType.Fire:
        return ActionFire;
      default:
        return null;
    }
  }
}