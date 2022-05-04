using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed, FloorOffset;
    public LayerMask FloorLayerMask;
    Transform _cameraTransform;
    Rigidbody _rb;
    Animator animator;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Move();
        StickFeetOnGround();
    }

    void Move()
    {
        Vector3 right = _cameraTransform.right * Input.GetAxis("Horizontal");
        Vector3 forward = _cameraTransform.forward * Input.GetAxis("Vertical");
        Vector3 FinalMove = new Vector3(forward.x + right.x, 0, forward.z + right.z);
        FinalMove /= Vector3.Distance(Vector3.zero, FinalMove);
        FinalMove *= MoveSpeed;
        if(right + forward != Vector3.zero)
        {
            transform.LookAt(transform.position + forward + right);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
        else { FinalMove = new Vector3(0, _rb.velocity.y, 0); }

        _rb.velocity = new Vector3(FinalMove.x, _rb.velocity.y, FinalMove.z);
        _rb.velocity = new Vector3(Mathf.Clamp(_rb.velocity.x, -MoveSpeed, MoveSpeed), 0, Mathf.Clamp(_rb.velocity.z, -MoveSpeed, MoveSpeed));
        _rb.angularVelocity = Vector3.zero;

        animator.SetFloat("Speed", Vector3.Distance(Vector3.zero, new Vector3(FinalMove.x, 0, FinalMove.z)));
    }

    void StickFeetOnGround()
    {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3f, FloorLayerMask);
        transform.position = new Vector3(transform.position.x, hit.point.y + FloorOffset, transform.position.z);
    }
}
