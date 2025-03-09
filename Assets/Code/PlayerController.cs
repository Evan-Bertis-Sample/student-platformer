using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        IDLE,
        WALKING,
        JUMPING
    }

    public float Speed;
    public float JumpForce;
    public float GroundRaycastDistance;
    public LayerMask GroundLayer;

    private Rigidbody2D _rb;

    [field: SerializeField] public PlayerState State { get; private set; }

    public Vector3 MoveInput { get; private set; }
    public bool JumpInput { get; private set; }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, GroundRaycastDistance, GroundLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * GroundRaycastDistance);
    }

    void Update()
    {
        MoveInput = new Vector3(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"),
                0
            );

        JumpInput = Input.GetKeyDown(KeyCode.Space);

        if (IsGrounded())
        {
            _rb.AddForce(new Vector2(0, JumpInput ? JumpForce : 0));
        }
        else
        {
            State = PlayerState.JUMPING;
            return;
        }

        State = (MoveInput.x == 0) ? PlayerState.IDLE : PlayerState.WALKING;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // transform.position += _moveInput * Speed * Time.fixedDeltaTime;
        transform.position += new Vector3(MoveInput.x, 0, 0) * Speed * Time.fixedDeltaTime;

    }
}
