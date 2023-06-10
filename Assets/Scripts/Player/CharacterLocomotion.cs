using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis
{
    Horizontal,
    Vertical
}
public class CharacterLocomotion : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    private Vector2 _input;
    public bool enableInput = true;

    private void Update()
    {
        if (enableInput)
        {
            _input.x = Input.GetAxis(Axis.Horizontal.ToString());
            _input.y = Input.GetAxis(Axis.Vertical.ToString());
        }
        else
        {
            _input = Vector2.zero;
        }


        if (_animator)
        {
            if (_input.x > 0)
                _animator.SetFloat("X", Mathf.Lerp(_animator.GetFloat("X"), 1f, Time.deltaTime * 9f));
            else if (_input.x < 0)
                _animator.SetFloat("X", Mathf.Lerp(_animator.GetFloat("X"), -1f, Time.deltaTime * 9f));
            else
                _animator.SetFloat("X", Mathf.Lerp(_animator.GetFloat("X"), 0f, Time.deltaTime * 9f));
            if (_input.y > 0)
                _animator.SetFloat("Z", Mathf.Lerp(_animator.GetFloat("Z"), 1f, Time.deltaTime * 9f));
            else if (_input.y < 0)
                _animator.SetFloat("Z", Mathf.Lerp(_animator.GetFloat("Z"), -1f, Time.deltaTime * 9f));
            else
                _animator.SetFloat("Z", Mathf.Lerp(_animator.GetFloat("Z"), 0f, Time.deltaTime * 9f));

        }
    }
}
