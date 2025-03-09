using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Sprite Animation")]
public class SpriteAnimation : ScriptableObject
{
    public List<Sprite> Frames = new List<Sprite>();
    public float FramesPerSecond = 12;
    public bool Loop = true;

    private int _index;
    private float _timeSinceLastFrame;
    private float _frameTime => 1 / FramesPerSecond;

    public void Reset() {
        _index = 0;
        _timeSinceLastFrame = Time.time;
    }

    public Sprite Play() {
        if (Time.time - _timeSinceLastFrame > _frameTime) {
            _timeSinceLastFrame = Time.time;
            // if we are looping, modulo
            if (Loop) {
                _index = (_index + 1) % Frames.Count;
            }
            else {
                // if we are not looping, clamp
                _index = Mathf.Clamp(_index + 1, 0, Frames.Count - 1);
            }
        }

        // return the current frame
        return Frames[_index];
    }
}
