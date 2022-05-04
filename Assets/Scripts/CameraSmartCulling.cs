using UnityEngine;

public class CameraSmartCulling : MonoBehaviour
{
    public Transform Target;
    public float distanceToChangeMask;
    public LayerMask NormalMask, PlayerlessMask;
    Camera myCam;

    void Start() => myCam = GetComponent<Camera>();

    void FixedUpdate() => myCam.cullingMask = Vector3.Distance(transform.position, Target.position) > distanceToChangeMask ? NormalMask : PlayerlessMask;
}
