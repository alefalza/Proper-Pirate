using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] private Controller initialController;

    public static ControllerManager Instance { get; private set; }
    public IController CurrentController { get; private set; }

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
        CurrentController = initialController;
    }

    private void Update()
    {
        CurrentController.HandleInputs();
    }

    private void FixedUpdate()
    {
        CurrentController.Move();
    }

    public void SetController(IController controller)
    {
        CurrentController = controller;
    }
}