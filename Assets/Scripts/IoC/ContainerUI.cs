using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lix.Core;

public class ContainerUI : DIContainerRegisterMono
{

  public override void RegisterDependencies()
  {
    // DIContainer.Register(new ServiceDescriptor(menuGameOver, ServiceLifetime.Transient));

    // DIContainer.Register(new ServiceDescriptor(gameManager, ServiceLifetime.Singleton));
  }
}
