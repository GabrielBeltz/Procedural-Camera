using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(BoxCollider))]
public class CameraVolume : MonoBehaviour
{
    [HideInInspector] public CameraVolumeType CameraVolumeType;
    [HideInInspector] public string TargetTag;

    [Header("Configs Globais")]
    public bool IsActive;
    Camera Camera;
    public MixingCameraController MixingCameraController;

    #region Unity Life Cycle

    void Awake()
    {
        Camera = Camera.main;
        GetComponent<Collider>().isTrigger = true;

        ValidateCamera();
    }

    void Update() => ExecuteBehaviour();
    
    void OnTriggerEnter(Collider other) => SetActivation(other.CompareTag(TargetTag));
    void OnTriggerExit(Collider other) => SetActivation(!other.CompareTag(TargetTag));

    #endregion

    void SetActivation(bool active) => IsActive = active;
    void ValidateCamera() => enabled = Camera != null;

    #region Behaviours

    void ExecuteBehaviour()
    {
        if (IsActive) MixingCameraController.CallBehaviour(CameraVolumeType);
    }

    #endregion
}

public enum CameraVolumeType { Fixated, Dolly, FirstPerson }