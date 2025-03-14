using UnityEngine;

public class RudderTrigger : MonoBehaviour
{
    [SerializeField] private Controller boatController;

    private Controller _playerController;
    private bool _isPlayerNearby = false;
    private bool _isPiloting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerController = other.GetComponent<Controller>();
            _isPlayerNearby = true;
            Debug.Log("Press [E] to pilot the boat");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerNearby = false;
        }
    }

    private void Update()
    {
        if (_isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            _isPiloting = !_isPiloting;
            IController controller = _isPiloting ? boatController : _playerController;
            ControllerManager.Instance.SetController(controller);
            EventService.Trigger(GameEvent.OnShipEnter, new object[] { _playerController, boatController });
        }
    }

    private static EventService _eventService;
    private static EventService EventService => _eventService ??= ServiceLocator.Instance.Get<EventService>();
}