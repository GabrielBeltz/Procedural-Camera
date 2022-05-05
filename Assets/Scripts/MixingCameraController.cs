using UnityEngine;
using System.Collections;
using Cinemachine;

public class MixingCameraController : MonoBehaviour
{
    public float Smoothing = 1;
    public CinemachineMixingCamera MixingCamera;
    bool _behaviourCalled;
    float step;
    IEnumerator ResetCameraCoroutine;

    private void Update() => step = Smoothing < 0.01f ? 1 : (1 / Smoothing) * Time.deltaTime;

    private void LateUpdate()
    {
        if(_behaviourCalled) return;
        IncreaseWeight(CameraVolumeType.FollowPlayer);
    }

    public void CallBehaviour(CameraVolumeType type)
    {
        IncreaseWeight(type);
        _behaviourCalled = true;
        if(ResetCameraCoroutine != null) StopCoroutine(ResetCameraCoroutine);
        ResetCameraCoroutine = ResetCamera();
        StartCoroutine(ResetCameraCoroutine);
    }

    void IncreaseWeight(CameraVolumeType type)
    {
        MixingCamera.m_Weight0 = Mathf.Clamp01(MixingCamera.m_Weight0 + (type == CameraVolumeType.FollowPlayer? step : -step));
        MixingCamera.m_Weight1 = Mathf.Clamp01(MixingCamera.m_Weight1 + (type == CameraVolumeType.Fixated? step : -step));
        MixingCamera.m_Weight2 = Mathf.Clamp01(MixingCamera.m_Weight2 + (type == CameraVolumeType.Dolly? step : -step));
        MixingCamera.m_Weight3 = Mathf.Clamp01(MixingCamera.m_Weight3 + (type == CameraVolumeType.FirstPerson? step : -step));
    }

    public void SetSmoothing(float value) => Smoothing = value;

    IEnumerator ResetCamera()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        _behaviourCalled = false;
    }
}
