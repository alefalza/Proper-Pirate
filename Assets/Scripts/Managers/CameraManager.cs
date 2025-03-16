using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera initialCamera;

    public static CameraManager Instance { get; private set; }
    public Camera CurrentCamera { get; private set; }

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
        CurrentCamera = initialCamera;
    }

    public void SetMainCamera(Camera camera)
    {
        CurrentCamera.gameObject.SetActive(false);
        camera.gameObject.SetActive(true);
        CurrentCamera = camera;
    }
}