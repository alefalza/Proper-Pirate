using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : Controller
{
    [Header("Boat Settings")]
    [SerializeField] private Transform rudder;
    [SerializeField] private float thrustPower = 1f;
    [SerializeField] private float maxThrustPower = 3f;
    [SerializeField] private float rudderTurnSpeed = 2f;
    [SerializeField] private float rudderMaxAngle = 30f;
    [SerializeField] private float thrustDecayRate = 1f;

    private Rigidbody _rigidbody;
    private float _currentThrustPower;
    private float _rudderRotationY;
    private float _waveHeight;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
}