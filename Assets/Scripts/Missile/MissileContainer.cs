using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Lix.Core;

public class MissileContainer : MonoBehaviour
{
  private IInputListener inputListener;
  private UpgradeManager upgradeManager;

  [SerializeField] private Transform missilePrefab;

  // Start is called before the first frame update
  void Start()
  {
    inputListener = DIContainer.GetService<IInputListener>();
    upgradeManager = DIContainer.GetService<UpgradeManager>();

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

    ReloadCoroutine();
  }

  public void ReloadCoroutine()
  {
    StartCoroutine(Reload());
  }

  IEnumerator Reload()
  {
    yield return new WaitForSeconds(upgradeManager.GetReloadDelay());

    InternalDebug.Log("Reloading");
    // Transform missile = Instantiate(missilePrefab, this.transform, false);
    GameObject missile = PoolManager.Get("MissilePool").Pool.Get();
    missile.transform.SetParent(this.transform);
    missile.transform.localPosition = Vector3.zero;
    missile.transform.localEulerAngles = Vector3.zero;
    missile.transform.localEulerAngles = Vector3.zero;
    missile.transform.localScale = Vector3.one;
    missile.SetActive(true);
  }

  private void OnDestroy()
  {
    inputListener.GetAction(InputActionType.Fire).performed -= OnFireInputPerformed;
  }
}
