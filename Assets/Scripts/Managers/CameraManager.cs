using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera initialCamera;

    private Camera _currentCamera;

    public static CameraManager Instance { get; private set; }

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
    }

    private void Start()
    {
        _currentCamera = initialCamera;
    }

    public void SetMainCamera(Camera camera)
    {
        _currentCamera.gameObject.SetActive(false);
        camera.gameObject.SetActive(true);
        _currentCamera = camera;
    }
}