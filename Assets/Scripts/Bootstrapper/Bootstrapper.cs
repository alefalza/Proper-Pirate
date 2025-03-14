using UnityEngine;
using UnityEngine.SceneManagement;

public static class Bootstrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        // Initialize Service Locator.
        ServiceLocator.Initialize();

        // Register all services.
        ServiceLocator.Instance.Register(new EventService());

        // Application is ready to start, load Main Scene.
        //SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
}