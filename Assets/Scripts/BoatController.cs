using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : Controller
{
    [Header("Thrust Settings")]
    [SerializeField] private float thrustPower = 1f;
    [SerializeField] private float maxThrustPower = 3f;
    [SerializeField] private float thrustDecayRate = 1f;
    
    [Header("Rudder Settings")]
    [SerializeField] private Transform rudder;
    [SerializeField] private float rudderTurnSpeed = 2f;
    [SerializeField] private float rudderMaxAngle = 30f;
    [SerializeField] private RudderTrigger rudderTrigger;
    [SerializeField] private Transform entryPoint;
    [SerializeField] private Transform exitPoint;

    private Rigidbody _rigidbody;
    private float _currentThrustPower;
    private float _rudderRotationY;
    private float _waveHeight;

    public bool IsPiloting { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        rudderTrigger.OnTrigger += OnRudderTriggered;
    }

    public override void HandleInputs()
    {
        HandleInput();
        RotateRudder();
    }

    public override void Move()
    {
        ApplyThrustForce();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _currentThrustPower = Mathf.Min(_currentThrustPower + thrustPower * Time.deltaTime, maxThrustPower);
        }
        else
        {
            _currentThrustPower = Mathf.Max(_currentThrustPower - thrustDecayRate * Time.deltaTime, 0f);
        }

        float rudderInput = 0f;
        
        if (Input.GetKey(KeyCode.A)) rudderInput = 1f;
        if (Input.GetKey(KeyCode.D)) rudderInput = -1f;

        _rudderRotationY += rudderInput * rudderTurnSpeed;
        _rudderRotationY = Mathf.Clamp(_rudderRotationY, -rudderMaxAngle, rudderMaxAngle);
    }

    private void RotateRudder()
    {
        rudder.localEulerAngles = new Vector3(0f, _rudderRotationY, 0f);
    }

    private void ApplyThrustForce()
    {
        _waveHeight = WaveManager.Instance.GetWaveHeight(rudder.position);
        
        if (rudder.position.y < _waveHeight)
        {
            Vector3 thrustForce = rudder.forward * _currentThrustPower;
            _rigidbody.AddForceAtPosition(thrustForce, rudder.position);
        }
    }

    private void OnRudderTriggered(PlayerController player)
    {
        IsPiloting = !IsPiloting;

        if (IsPiloting)
        {
            player.transform.SetParent(entryPoint);
            player.ForcePositionAndRotation(entryPoint);
            CameraManager.Instance.SetMainCamera(mainCamera);
        }
        else
        {
            player.transform.SetParent(null);
            player.ForcePositionAndRotation(exitPoint);
            CameraManager.Instance.SetMainCamera(player.Camera);
        }

        IController controller = IsPiloting ? this : player;
        ControllerManager.Instance.SetController(controller);
    }

    private void OnDestroy()
    {
        rudderTrigger.OnTrigger -= OnRudderTriggered;
    }
}