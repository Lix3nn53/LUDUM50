using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Lix.Core;

public class MissileContainer : MonoBehaviour
{

  private IInputListener inputListener;
  // Start is called before the first frame update
  void Start()
  {


    inputListener = DIContainer.GetService<IInputListener>();

    InputAction fireAction = inputListener.GetAction(InputActionType.Fire);
    fireAction.performed += OnFireInputPerformed;
  }

  private void OnFireInputPerformed(InputAction.CallbackContext context)
  {
    int missileCount = transform.childCount;

    if (missileCount < 1)
    {
      return;
    }

    transform.GetChild(0).GetComponent<Missile>().Fire();
  }
}
