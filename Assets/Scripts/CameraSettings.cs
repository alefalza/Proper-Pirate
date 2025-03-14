using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCameraSettings", menuName = "Game Settings/Camera Settings")]
public class CameraSettings : ScriptableObject
{
    [Header("Rotation")]
    public Vector2 verticalLimits = new Vector2(-45f, 45f);
    public Vector2 mouseSensitivity = new Vector2(3.5f, 3.5f);
    public bool invertVerticalRotation = true;

    [Header("Smoothing")]
    [Range(0f, 0.5f)] public float mouseSmoothTime = 0.03f;

    [Header("Cursor")]
    public bool lockCursor = true;

    [Header("Ray")]
    public float rayDistance = 100f;
}