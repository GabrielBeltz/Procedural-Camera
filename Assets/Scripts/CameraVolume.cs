using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(BoxCollider))][HideInInspector]
public class CameraVolume : MonoBehaviour
{
    [HideInInspector] public CameraVolumeType CameraVolumeType;
    [HideInInspector] public Transform Target;
    [HideInInspector] public Vector3 Angle, Position;
    [Header("Configs Globais")][Range(10f, 120f)] public float FieldOfView = 60;
    [Range(-0.5f, 1.5f)] public float ScreenX = 0.5f, ScreenY = 0.5f;
    public Camera Camera;

    void Awake()
    {
        Camera ??= Camera.main;
    }

    void Update()
    {
        switch(CameraVolumeType)
        {
            case CameraVolumeType.FixatedLookAt:
                FixatedLookAtBehaviour();
                break;
            case CameraVolumeType.FixatedAngleFollow:
                FixatedAngleFollowBehaviour();
                break;
        }
    }

    void FixatedLookAtBehaviour()
    {
        Debug.LogWarning("Não implementado.", this);
    }

    void FixatedAngleFollowBehaviour()
    {
        Debug.LogWarning("Não implementado.", this);
    }
}

public enum CameraVolumeType { FixatedLookAt, FixatedAngleFollow }