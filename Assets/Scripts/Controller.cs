using UnityEngine;

public class Controller : MonoBehaviour, IController
{
    [SerializeField] protected Camera mainCamera;

    public Camera Camera => mainCamera;

    public virtual void HandleInputs() { }

    public virtual void Move() { }
}