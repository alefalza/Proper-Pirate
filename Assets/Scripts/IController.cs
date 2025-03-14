using UnityEngine;

public interface IController
{
    Camera Camera { get; }
    void HandleInputs();
    void Move();
}