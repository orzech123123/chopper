using System;
using UnityEngine;

namespace Assets.Scripts.Chopper
{
    [RequireComponent(typeof(Rigidbody))]
    public class FlightController : MonoBehaviour
    {
        public float MaxVelocity = 10f;
        public float Force = 25f;
        public Joystick LeftJoystick;
        public Joystick RightJoystick;
        private Rigidbody _rigidBody;
        private float? _keepAltitude;

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _rigidBody.maxAngularVelocity = 10.5f;
        }

        void FixedUpdate()
        {
            var forwardVelocity = Vector3.Dot(_rigidBody.velocity, transform.forward);
            var altitudeChangedByInput = false;

            if (Math.Abs(LeftJoystick.Vertical) > 0.01f)
            {
                _rigidBody.AddForce(transform.up * Force * LeftJoystick.Vertical, ForceMode.Acceleration);
                _keepAltitude = null;
                altitudeChangedByInput = true;
            }

            if (Math.Abs(LeftJoystick.Horizontal) > 0.01f)
            {
                _rigidBody.AddForce(transform.right * 7f * LeftJoystick.Horizontal, ForceMode.Acceleration);
            }

            if (Math.Abs(RightJoystick.Vertical) > 0.01f && Math.Abs(forwardVelocity) < MaxVelocity)
            {
                _rigidBody.AddForce(transform.forward * 8f * RightJoystick.Vertical, ForceMode.Acceleration);
            }
            if (Math.Abs(RightJoystick.Horizontal) > 0.01f)
            {
                _rigidBody.AddTorque(transform.up * 0.2f * RightJoystick.Horizontal, ForceMode.Acceleration);
            }

            if (!altitudeChangedByInput)
            {
                if (_keepAltitude == null)
                {
                    _keepAltitude = transform.position.y;
                }

                var goHigher = transform.position.y <= _keepAltitude ? 1 : -1;
                _rigidBody.AddForce(transform.up * Force * goHigher, ForceMode.Acceleration);
            }
        }
    }
}   