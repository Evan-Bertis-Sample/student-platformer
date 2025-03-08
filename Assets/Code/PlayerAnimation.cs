using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public List<Sprite> WalkingAnimation = new List<Sprite>();
    public List<Sprite> IdleAnimation = new List<Sprite>();

    public SpriteRenderer Renderer;
    public PlayerController Controller;

    public float FramesPerSecond = 12;
    private int _index;
    private float _timeSinceLastFrame;

    private float FrameTime => 1 / FramesPerSecond;


    private List<Sprite> _currentAnimation = new List<Sprite>();

    public void Start() {
        _index = 0;
    }

    public void Update() {
        _currentAnimation = (Controller.State == PlayerController.PlayerState.IDLE) ? 
            IdleAnimation : WalkingAnimation;

        if (Time.time - _timeSinceLastFrame > FrameTime) {
            _index = (_index + 1 ) % _currentAnimation.Count;
            _timeSinceLastFrame = Time.time;
        }


        Renderer.sprite = _currentAnimation[_index];
        
        if (Controller.MoveInput.x != 0)
            Renderer.flipX = (Controller.MoveInput.x < 0);
    }
}
