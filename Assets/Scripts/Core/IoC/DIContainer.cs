using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Lix.Core
{
  public class DIContainer : MonoBehaviour
  {
    private static IDictionary<Type, ServiceDescriptor> serviceDescriptors = new Dictionary<Type, ServiceDescriptor>();

    private static IDictionary<Type, ServiceDescriptor> GetServiceDescriptors()
    {
      return serviceDescriptors;
    }

    public static void Register(ServiceDescriptor serviceDescriptor)
    {
      IDictionary<Type, ServiceDescriptor> serviceDescriptors = GetServiceDescriptors();

      if (serviceDescriptor.ServiceLifetime == ServiceLifetime.Singleton) // Dont register singleton if already registered
      {
        if (serviceDescriptors.ContainsKey(serviceDescriptor.ServiceType))
        {
          // throw new Exception(string.Format("Service {0} is already registered", serviceDescriptor.ServiceType));
          InternalDebug.LogWarning(string.Format("Service {0} is already registered", serviceDescriptor.ServiceType));
          Destroy(((MonoBehaviour)serviceDescriptor.Implementation).gameObject);
          return;
        }
      }
      InternalDebug.Log(string.Format("New service {0} is registered!", serviceDescriptor.ServiceType), (MonoBehaviour)serviceDescriptor.Implementation);

      serviceDescriptors.Remove(serviceDescriptor.ServiceType);
      serviceDescriptors.Add(serviceDescriptor.ServiceType, serviceDescriptor);
      // serviceDescriptors[serviceDescriptor.ServiceType] = serviceDescriptor;

      if (serviceDescriptor.ServiceLifetime == ServiceLifetime.Singleton)
      {
        DontDestroyOnLoad(((MonoBehaviour)serviceDescriptor.Implementation).gameObject);
      }
    }

    private static object GetService(Type serviceType)
    {
      IDictionary<Type, ServiceDescriptor> serviceDescriptors = GetServiceDescriptors();
      var serviceDescriptor = serviceDescriptors[serviceType];

      if (serviceDescriptor == null)
      {
        throw new Exception($"Service of type {serviceType.Name} is not registered");
      }

      // Return implementation if it is already created
      if (serviceDescriptor.Implementation != null)
      {
        return serviceDescriptor.Implementation;
      }

      // Create implementation if it is not created yet
      Type actualType = serviceDescriptor.ImplementationType ?? serviceDescriptor.ServiceType;
      if (actualType.IsAbstract || actualType.IsInterface)
      {
        throw new Exception($"Can't create instance of abstract or interface type {actualType.Name}");
      }

      System.Reflection.ConstructorInfo constructorInfo = actualType.GetConstructors()[0];

      object[] parameters = constructorInfo.GetParameters().Select(parameter => GetService(parameter.ParameterType)).ToArray();

      var implementation = Activator.CreateInstance(actualType, parameters); // constructorInfo.Invoke(parameters); 

      // Save implementation if it is singleton
      //if (serviceDescriptor.ServiceLifetime == ServiceLifetime.Singleton)
      serviceDescriptor.Implementation = implementation;

      return implementation;
    }

    public static T GetService<T>()
    {
      return (T)GetService(typeof(T));
    }
  }
}