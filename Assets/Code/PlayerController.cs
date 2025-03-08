using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState {
        IDLE, WALKING
    }

    public float Speed;
    [field:SerializeField] public PlayerState State { get; private set; }

    public Vector3 MoveInput { get; private set;}

    void Update()
    {
        MoveInput = new Vector3(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"),
                0
            );

        State = (MoveInput.x == 0) ? PlayerState.IDLE : PlayerState.WALKING;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // transform.position += _moveInput * Speed * Time.fixedDeltaTime;
        transform.position += new Vector3(MoveInput.x, 0, 0) * Speed * Time.fixedDeltaTime;
    }
}
