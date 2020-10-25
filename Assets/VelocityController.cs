using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocityController : MonoBehaviour
{
    [SerializeField]
    private int _force = 250;

    [SerializeField]
    private Joystick _joystick;

    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Math.Abs(_joystick.Vertical) > 0.01f)
        {
            _rigidbody.AddForce(transform.up * _force * _joystick.Vertical, ForceMode.Acceleration);
        }
        if (Math.Abs(_joystick.Horizontal) > 0.01f)
        {
            _rigidbody.AddTorque(transform.up * _joystick.Horizontal, ForceMode.Acceleration);
        }

        if (Input.GetMouseButton(0) && Input.mousePosition.x > Screen.width / 2f)
        {
            _rigidbody.AddForce(transform.forward * 5f, ForceMode.Acceleration);
        }

        // Debug.Log(_joystick.Horizontal + " " + _joystick.Vertical);
    }
}   