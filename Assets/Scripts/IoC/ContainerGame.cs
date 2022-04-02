using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lix.Core;

public class ContainerGame : DIContainerRegisterMono
{
  [SerializeField] private World world;

  public override void RegisterDependencies()
  {
    DIContainer.Register(new ServiceDescriptor(world, ServiceLifetime.Transient));

    // DIContainer.Register(new ServiceDescriptor(gameManager, ServiceLifetime.Singleton));
  }
}
