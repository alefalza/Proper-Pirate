using UnityEngine;

public class CameraPivotController : MonoBehaviour
{
    [SerializeField] private CameraSettings settings;

    private float yRotation;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        yRotation += InputManager.Instance.MouseInput.x * settings.mouseSensitivity.x;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}