using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 KeyboardInput { get; private set; }
    public Vector2 MouseInput { get; private set; }

    public static InputManager Instance { get; private set; }

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

    private void Update()
    {
        SetKeyboardInput();
        SetMouseInput();
    }

    private void SetKeyboardInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        KeyboardInput = new Vector2(horizontalInput, verticalInput);
    }

    private void SetMouseInput()
    {
        float xInput = Input.GetAxis("Mouse X");
        float yInput = Input.GetAxis("Mouse Y");

        MouseInput = new Vector2(xInput, yInput);
    }
}