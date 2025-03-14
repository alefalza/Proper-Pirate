using UnityEngine;

public class BoatManager : MonoBehaviour
{
    public static BoatManager Instance { get; private set; }
    public bool IsPiloting { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }

        EventService.SubscribeTo(GameEvent.OnShipEnter, OnShipEnter);
    }

    private void OnShipEnter(object[] parameters)
    {
        IsPiloting = !IsPiloting;
        PlayerController player = parameters[0] as PlayerController;
        BoatController boat = parameters[1] as BoatController;

        if (IsPiloting)
        {
            Vector3 moveDirection = boat.EntryPoint.position - player.transform.position;
            player.MoveTowards(moveDirection);
            player.transform.SetParent(boat.transform);
            CameraManager.Instance.SetMainCamera(boat.Camera);
        }
        else
        {
            player.transform.SetParent(null);
            Vector3 moveDirection = boat.ExitPoint.position - player.transform.position;
            player.MoveTowards(moveDirection);
            CameraManager.Instance.SetMainCamera(player.Camera);
        }
    }

    private void OnDestroy()
    {
        EventService.UnsubscribeFrom(GameEvent.OnShipEnter, OnShipEnter);
    }

    private static EventService _eventService;
    private static EventService EventService => _eventService ??= ServiceLocator.Instance.Get<EventService>();
}