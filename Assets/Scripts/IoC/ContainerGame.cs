using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lix.Core;

public class ContainerGame : DIContainerRegisterMono
{
  [SerializeField] private GameManager gameManager;
  [SerializeField] private World world;
  [SerializeField] private MissileContainer missileContainer;

  public override void RegisterDependencies()
  {
    DIContainer.Register(new ServiceDescriptor(gameManager, ServiceLifetime.Singleton));
    DIContainer.Register(new ServiceDescriptor(world, ServiceLifetime.Transient));
    DIContainer.Register(new ServiceDescriptor(missileContainer, ServiceLifetime.Transient));

    // DIContainer.Register(new ServiceDescriptor(gameManager, ServiceLifetime.Singleton));
  }
}
