using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraSettings settings;
    [SerializeField] private Transform target;

    private Transform _transform;
    private Vector2 _currentMouseDelta;
    private Vector2 _currentMouseDeltaVelocity;
    private Vector3 _targetRotation;
    private float _cameraPitch;

    public Vector3 HitPoint { get; private set; }

    private void Awake()
    {
        _transform = transform;

        if (settings.lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        _targetRotation = GetTargetRotation(InputManager.Instance.MouseInput);
        RotateController();
        DrawRay();
    }

    private void LateUpdate()
    {
        _transform.localEulerAngles = _targetRotation;
    }

    private Vector3 GetTargetRotation(Vector3 mouseInput)
    {
        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, mouseInput, ref _currentMouseDeltaVelocity, settings.mouseSmoothTime);

        _cameraPitch -= _currentMouseDelta.y * settings.mouseSensitivity.x;
        _cameraPitch = Mathf.Clamp(_cameraPitch, settings.verticalLimits.x, settings.verticalLimits.y);

        return Vector3.right * _cameraPitch;
    }

    private void RotateController()
    {
        float eulerAngles = _currentMouseDelta.x * settings.mouseSensitivity.y;
        target.Rotate(Vector3.up * eulerAngles);
    }

    private void DrawRay()
    {
        bool rayHit = Physics.Raycast(_transform.position, _transform.forward, out RaycastHit hitInfo, settings.rayDistance);
        HitPoint = rayHit ? hitInfo.point : _transform.forward * settings.rayDistance;
    }

    private void OnDrawGizmosSelected()
    {
        if (_transform == null || settings == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawRay(_transform.position, _transform.forward * settings.rayDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(HitPoint, 0.1f);
    }
}