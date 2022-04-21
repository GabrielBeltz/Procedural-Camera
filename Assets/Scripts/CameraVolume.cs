using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraVolume : MonoBehaviour
{
    [HideInInspector] public CameraVolumeType CameraVolumeType;
    // Vari�veis que s�o usadas ou n�o dependendo do tipo de c�mera | Sim, s�o vari�veis que tem uso variado | Vari�veis vari�veis
    [HideInInspector] public Transform Target;
    [HideInInspector] public Vector3 PositionA, PositionB;
    [HideInInspector] public Quaternion Rotation;
    [HideInInspector][Range(-0.5f, 1.5f)] public float ScreenX = 0.5f, ScreenY = 0.5f;

    [Header("Configs Globais")]
    [SerializeField] bool IsActive;
    [Range(10f, 120f)] public float FieldOfView = 60;
    public Camera Camera;
    [HideInInspector] public string TargetTag;
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
    
    void OnTriggerExit(Collider other) => SetActivation(!other.CompareTag(TargetTag));

    void SetActivation(bool active) => IsActive = active;

    void OnEnable() => ExecuteBehaviour();

    void ValidateCamera() => enabled = Camera != null;

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
        _virtualCamera.ForceCameraPosition(PositionA, Rotation);
    }

    void FollowPlayerBehaviour()
    {
    }

    void DollyBehaviour()
    {
        // Achar a posi��o entre 2 pontos em 3D para colocar a c�mera
        // Ideia 1: Simplificar a posi��o 3D em 2D usando a dire��o da linha como o eixo fixo, e posicionando a altura e dist�ncia do ponto A
        if (Target == null) return;


        // Dire��o da linha
        Vector3 normal = PositionA - PositionB;
        // Tamanho da linha
        float size = Vector3.Distance(Vector3.zero, normal);


    }

    void FirstPersonBehaviour()
    {
    }

    #endregion
}

public enum CameraVolumeType { Fixated, FollowPlayer, Dolly, FirstPerson }