using UnityEngine;

[RequireComponent(typeof(BoxCollider))][HideInInspector]
public class CameraVolume : MonoBehaviour
{
    [HideInInspector] public CameraVolumeType CameraVolumeType;
    [Header("Configs Globais")][Range(10f, 120f)] public float FieldOfView = 60;
    [HideInInspector] public Transform Target;
    [Range(-0.5f, 1.5f)] public float ScreenX = 0.5f, ScreenY = 0.5f;
    [HideInInspector] public Vector3 Angle, Position;
}

public enum CameraVolumeType { FixatedLookAt, FixatedAngleFollow }