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
        MixingCamera.m_Weight0 = Mathf.Clamp01(MixingCamera.m_Weight0 + step);
        MixingCamera.m_Weight1 = Mathf.Clamp01(MixingCamera.m_Weight1 - step);
        MixingCamera.m_Weight2 = Mathf.Clamp01(MixingCamera.m_Weight2 - step);
        MixingCamera.m_Weight3 = Mathf.Clamp01(MixingCamera.m_Weight3 - step);
    }

    public void CallBehaviour(CameraVolumeType type)
    {
        switch (type)
        {
            case CameraVolumeType.Fixated:
                MixingCamera.m_Weight0 = Mathf.Clamp01(MixingCamera.m_Weight0 - step);
                MixingCamera.m_Weight1 = Mathf.Clamp01(MixingCamera.m_Weight1 + step);
                MixingCamera.m_Weight2 = Mathf.Clamp01(MixingCamera.m_Weight2 - step);
                MixingCamera.m_Weight3 = Mathf.Clamp01(MixingCamera.m_Weight3 - step);
                break;
            case CameraVolumeType.Dolly:
                MixingCamera.m_Weight0 = Mathf.Clamp01(MixingCamera.m_Weight0 - step);
                MixingCamera.m_Weight1 = Mathf.Clamp01(MixingCamera.m_Weight1 - step);
                MixingCamera.m_Weight2 = Mathf.Clamp01(MixingCamera.m_Weight2 + step);
                MixingCamera.m_Weight3 = Mathf.Clamp01(MixingCamera.m_Weight3 - step);
                break;
            case CameraVolumeType.FirstPerson:
                MixingCamera.m_Weight0 = Mathf.Clamp01(MixingCamera.m_Weight0 - step);
                MixingCamera.m_Weight1 = Mathf.Clamp01(MixingCamera.m_Weight1 - step);
                MixingCamera.m_Weight2 = Mathf.Clamp01(MixingCamera.m_Weight2 - step);
                MixingCamera.m_Weight3 = Mathf.Clamp01(MixingCamera.m_Weight3 + step);
                break;
        }

        _behaviourCalled = true;
        if(ResetCameraCoroutine != null) StopCoroutine(ResetCameraCoroutine);
        ResetCameraCoroutine = ResetCamera();
        StartCoroutine(ResetCameraCoroutine);
    }

    IEnumerator ResetCamera()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        _behaviourCalled = false;
    }
}
