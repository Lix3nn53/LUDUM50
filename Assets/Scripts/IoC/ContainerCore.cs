using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lix.Core;

public class ContainerCore : DIContainerRegisterMono
{
  [SerializeField] private InputListener inputListener;
  [SerializeField] private AudioManager audioManager;

  public override void RegisterDependencies()
  {
    DIContainer.Register(new ServiceDescriptor(audioManager, ServiceLifetime.Singleton));

    if (inputListener != null)
    {
      DIContainer.Register(new ServiceDescriptor(inputListener, typeof(IInputListener), ServiceLifetime.Singleton));
    }
  }
}
