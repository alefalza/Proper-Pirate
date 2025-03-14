using UnityEngine;

public class Controller : MonoBehaviour, IController
{
    [SerializeField] private Camera mainCamera;

    public Camera Camera => mainCamera;

    public virtual void HandleInputs() { }

    public virtual void Move() { }
}