using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraVolume : MonoBehaviour
{
    [HideInInspector] public CameraVolumeType CameraVolumeType;
    // Variáveis que são usadas ou não dependendo do tipo de câmera | Sim, são variáveis que tem uso variado | Variáveis variáveis
    [HideInInspector] public Transform Target;
    [HideInInspector] public Vector3 Angle, PositionA, PositionB;
    [HideInInspector][Range(-0.5f, 1.5f)] public float ScreenX = 0.5f, ScreenY = 0.5f;

    [Header("Configs Globais")]
    [SerializeField] bool IsActive;
    [Range(10f, 120f)] public float FieldOfView = 60;
    public Camera Camera;
    public string TargetTag;
    CinemachineVirtualCamera _virtualCamera;

    void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        Camera = Camera != null ? Camera : Camera.main;

        ValidateCamera();
    }

    void Update()
    {
        if (!IsActive) return;

        ExecuteBehaviour();
    }
    
    void OnTriggerEnter(Collider other) => SetActivation(other.CompareTag(TargetTag));
    
    void OnTriggerExit(Collider other) => SetActivation(other.CompareTag(TargetTag));

    void SetActivation(bool active) => IsActive = active;

    void OnEnable() => ExecuteBehaviour();

    void ValidateCamera() => enabled = Camera != null;

    void OnDisable() => Debug.LogWarning($"{gameObject.name} desativado. Isso pode ter sido causado pela referência perdida de câmera.");

    #region Behaviours

    void ExecuteBehaviour()
    {
        GlobalBehaviour();

        switch (CameraVolumeType)
        {
            case CameraVolumeType.Fixated:
                FixatedLookAtBehaviour();
                break;
            case CameraVolumeType.FollowPlayer:
                FollowPlayerBehaviour();
                break;
            case CameraVolumeType.Dolly:
                DollyBehaviour();
                break;
            case CameraVolumeType.FirstPerson:
                FirstPersonBehaviour();
                break;
        }
    }

    void GlobalBehaviour()
    { 
        ValidateCamera();
        Camera.fieldOfView = FieldOfView;
    }

    void FixatedLookAtBehaviour()
    {
    }

    void FollowPlayerBehaviour()
    {
    }

    void DollyBehaviour()
    {
    }

    void FirstPersonBehaviour()
    {
    }

    #endregion
}

public enum CameraVolumeType { Fixated, FollowPlayer, Dolly, FirstPerson }