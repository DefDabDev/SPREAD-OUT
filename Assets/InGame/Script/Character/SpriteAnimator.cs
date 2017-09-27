using System;
using System.Collections;
using System.Collections.Generic;
using AL.ALLog;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SpriteAnimation
{
    public string name;
    public float fps;
    public bool isLoop;
    public Sprite[] clips;
}

public class SpriteAnimator : MonoBehaviour {

    [SerializeField]
    private List<SpriteAnimation> _animationState = null;

    private SpriteAnimation _currentAnimation = null;
    private Image _image = null;
    private float _timer = 0f;
    private uint _index = 0;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void ChangeState(string name)
    {
        _index = 0;
        _timer = 0f;
        
        for (int i = 0; i < _animationState.Count; ++i)
        {
            if (_animationState[i].name.Equals(name))
            {
                _currentAnimation = _animationState[i];
                return;
            }
        }

        ALLog.ErrorLog(string.Format("Can't find state! : {0}", name));
    }

    public void ChangeStateNotInit(string name)
    {
        for (int i = 0; i < _animationState.Count; ++i)
        {
            if (_animationState[i].name.Equals(name))
            {
                _currentAnimation = _animationState[i];
                return;
            }
        }

        ALLog.ErrorLog(string.Format("Can't find state! : {0}", name));
    }

    private void Update()
    {
        if (_currentAnimation == null)
            return;

        _timer += Time.deltaTime;
        if (_timer > _currentAnimation.fps)
        {
            _timer = 0f;
            ++_index;
            if (_currentAnimation.clips.Length <= _index)
            {
                if (_currentAnimation.isLoop)
                    _index = 0; 
                else
                    --_index;
            }
            _image.sprite = _currentAnimation.clips[_index];
        }
    }
}
