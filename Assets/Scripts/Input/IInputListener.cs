using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InputActionType { Move, Pause, Debug }
public interface IInputListener
{
  InputAction GetAction(InputActionType type);
}