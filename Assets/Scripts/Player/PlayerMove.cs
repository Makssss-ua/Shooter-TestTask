using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 5f;
    [SerializeField] private float _runModifier = 1.2f;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _jumpLayer;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;

    private Transform _transform;
    private float _speedAnim;
    private float _velocity;

    private float _gravity;

    private void Start()
    {
        if(!_characterController)
            _characterController = GetComponent<CharacterController>();
        _transform = transform;
    }

    private void FixedUpdate()
    {
        float speed = _playerSpeed;
        float x = Input.GetAxis(Axis.Horizontal.ToString());
        float z = Input.GetAxis(Axis.Vertical.ToString());

        _speedAnim = Mathf.Abs(x) + Mathf.Abs(z);
        _speedAnim = Mathf.Clamp(_speedAnim, 0f, 1f);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= _runModifier;
            _speedAnim *= _runModifier;
        }

        bool isGrounded = Physics.CheckSphere(_transform.position, 0.25f, _jumpLayer);
        if (!isGrounded)
        {
            _gravity += MasterManager.LevelSettings.mapGravity * Time.deltaTime;
        }
        else
        {
            if (_gravity <= 0)
            {
                _gravity = MasterManager.LevelSettings.mapGravity;
            }
        }
        if (Input.GetButton("Jump") && isGrounded)
        {
            _gravity = _jumpForce;
        }

        _speedAnim = Mathf.SmoothDamp(_animator.GetFloat("Speed"), _speedAnim, ref _velocity, 0.1f);
        _animator.SetFloat("Speed", _speedAnim);

        Vector3 move = _transform.right * x + _transform.up * _gravity + _transform.forward * z;

        _characterController.Move(move * speed * Time.deltaTime);
    }
}
