using UnityEngine;

namespace Lix.Core
{
  public abstract class DIContainerRegisterMono : MonoBehaviour, IDIContainerRegister
  {
    private void Awake()
    {
      RegisterDependencies();
    }

    public abstract void RegisterDependencies();
  }
}