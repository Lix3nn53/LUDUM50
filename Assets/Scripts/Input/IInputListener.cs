using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InputActionType { Move, Pause, Fire }
public interface IInputListener
{
  InputAction GetAction(InputActionType type);
}