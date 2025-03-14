using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Service Locator for <see cref="ILocatable"/> instances.
/// </summary>
public class ServiceLocator
{
    /// <summary>
    /// Currently registered services.
    /// </summary>
    private readonly Dictionary<string, ILocatable> _services = null;

    /// <summary>
    /// Currently active Service Locator Instance.
    /// </summary>
    public static ServiceLocator Instance { get; private set; } = null;

    // Private constructor to avoid misuse of the class by creating new instances of it randomly.
    private ServiceLocator()
    {
        _services = new Dictionary<string, ILocatable>();
    }

    /// <summary>
    /// Initializes the Service Locator with a new instance.
    /// </summary>
    public static void Initialize()
    {
        Instance = new ServiceLocator();
    }

    /// <summary>
    /// Gets the service instance of the given type.
    /// </summary>
    /// <typeparam name="T">Service type.</typeparam>
    /// <returns>Service instance.</returns>
    public T Get<T>() where T : ILocatable
    {
        string key = typeof(T).Name;

        if (!_services.ContainsKey(key))
        {
            Debug.LogError($"{key} is not registered to the {GetType().Name}");
            throw new InvalidOperationException();
        }

        return (T)_services[key];
    }

    /// <summary>
    /// Registers the service to the current Service Locator.
    /// </summary>
    /// <typeparam name="T">Service type.</typeparam>
    /// <param name="service">Service instance.</param>
    public void Register<T>(T service) where T : ILocatable
    {
        string key = typeof(T).Name;

        if (_services.ContainsKey(key))
        {
            Debug.LogError($"Attempted to register a service of type {key} which has already been registered to the {GetType().Name}.");
            return;
        }

        _services.Add(key, service);
    }

    /// <summary>
    /// Unregisters the service from the current Service Locator.
    /// </summary>
    /// <typeparam name="T">Service type.</typeparam>
    public void Unregister<T>() where T : ILocatable
    {
        string key = typeof(T).Name;

        if (!_services.ContainsKey(key))
        {
            Debug.LogError($"Attempted to unregister a service of type {key} which has not been registered to the {GetType().Name}.");
            return;
        }

        _services.Remove(key);
    }
}