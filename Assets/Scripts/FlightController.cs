using System;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class FlightController : MonoBehaviour
    {
        [SerializeField]
        private int _force = 250;
        [SerializeField]
        private Joystick _leftJoystick;
        [SerializeField]
        private Joystick _rightJoystick;
        private Rigidbody _rigidBody;

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            if (Math.Abs(_leftJoystick.Vertical) > 0.01f)
            {
                _rigidBody.AddForce(transform.up * _force * _leftJoystick.Vertical, ForceMode.Acceleration);
            }
            if (Math.Abs(_leftJoystick.Horizontal) > 0.01f)
            {
                _rigidBody.AddForce(transform.right * 7f * _leftJoystick.Horizontal, ForceMode.Acceleration);
            }

            if (Math.Abs(_rightJoystick.Vertical) > 0.01f)
            {
                _rigidBody.AddForce(transform.forward * 10f * _rightJoystick.Vertical, ForceMode.Acceleration);
            }
            if (Math.Abs(_rightJoystick.Horizontal) > 0.01f)
            {
                _rigidBody.AddTorque(transform.up * _rightJoystick.Horizontal, ForceMode.Acceleration);
            }
        }
    }
}   