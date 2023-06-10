using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 5f;

    private Transform _transform;
    private Transform _camera;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _camera = Camera.main.transform;
        _transform = transform;
    }

    void FixedUpdate() 
    {
        _transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.Euler(0f, _camera.rotation.eulerAngles.y, 0f), turnSpeed * Time.deltaTime);
    }
}
