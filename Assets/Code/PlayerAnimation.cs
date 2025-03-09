using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(PlayerController))]
public class PlayerAnimation : MonoBehaviour
{
    public SpriteAnimation IdleAnimation;
    public SpriteAnimation WalkAnimation;
    public SpriteAnimation JumpAnimation;

    private SpriteAnimation _instanceIdleAnimation;
    private SpriteAnimation _instanceWalkAnimation;
    private SpriteAnimation _instanceJumpAnimation;

    public SpriteRenderer Renderer;
    public PlayerController Controller;

    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Controller = GetComponent<PlayerController>();

        _instanceIdleAnimation = Instantiate(IdleAnimation);
        _instanceWalkAnimation = Instantiate(WalkAnimation);
        _instanceJumpAnimation = Instantiate(JumpAnimation);
    }

    void Update() {
        SpriteAnimation currentAnimation = null;
        switch (Controller.State) {
            case PlayerController.PlayerState.IDLE:
                currentAnimation = _instanceIdleAnimation;
                break;
            case PlayerController.PlayerState.WALKING:
                currentAnimation = _instanceWalkAnimation;
                break;
            case PlayerController.PlayerState.JUMPING:
                currentAnimation = _instanceJumpAnimation;
                break;
        }

        Renderer.sprite = currentAnimation.Play();
        
        if (Controller.MoveInput.x != 0)
            Renderer.flipX = (Controller.MoveInput.x < 0);

        if (Controller.IsGrounded()) {
            _instanceJumpAnimation.Reset();
        }
    }
}
