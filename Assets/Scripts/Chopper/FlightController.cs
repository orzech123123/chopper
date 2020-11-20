using System;
using UnityEngine;

namespace Assets.Scripts.Chopper
{
    [RequireComponent(typeof(Rigidbody))]
    public class FlightController : MonoBehaviour
    {
        public float MaxAngularVelocity = 10f;
        public float MaxVelocity = 10f;
        public float Force = 25f;

        public Joystick LeftJoystick;
        public Joystick RightJoystick;

        private Rigidbody _rigidBody;

        public float VerticalSlowDownTotalTime = 5f;
        private float _verticalSlowDownDiffTime;

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _rigidBody.maxAngularVelocity = MaxAngularVelocity;
        }

        void FixedUpdate()
        {
            var forwardVelocity = Vector3.Dot(_rigidBody.velocity, transform.forward);
            var wasHeightChanged = false;

            if (Math.Abs(LeftJoystick.Vertical) > 0.01f)
            {
                _rigidBody.AddForce(transform.up * Force * LeftJoystick.Vertical, ForceMode.Acceleration);

                wasHeightChanged = true;
                _verticalSlowDownDiffTime = 0;
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

            if (!wasHeightChanged)
            {
                SlowDownVertically();
            }
        }

        private void SlowDownVertically()
        {
            _verticalSlowDownDiffTime += Time.deltaTime / VerticalSlowDownTotalTime;
            var factor = Mathf.Lerp(0, 1, _verticalSlowDownDiffTime);

            var v = _rigidBody.velocity;
            var yCounterVelocity = -v.y * factor;
            _rigidBody.AddForce(new Vector3(0, yCounterVelocity, 0), ForceMode.VelocityChange);
        }
    }
}   