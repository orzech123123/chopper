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

        public float RotationSlowDownTotalTime = 5f;
        private float _rotationSlowDownDiffTime;

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _rigidBody.maxAngularVelocity = MaxAngularVelocity;
        }

        void FixedUpdate()
        {
            MoveVertically();

            MoveLeftAndRight();

            MoveForward();

            RotateOnYAxis();
        }

        private void RotateOnYAxis()
        {
            if (Math.Abs(RightJoystick.Horizontal) > 0.01f)
            {
                _rigidBody.AddTorque(transform.up * 0.2f * RightJoystick.Horizontal, ForceMode.Acceleration);
                _rotationSlowDownDiffTime = 0;
            }
            else
            {
                SlowDownRotation();
            }
        }

        private void MoveVertically()
        {
            if (Math.Abs(LeftJoystick.Vertical) > 0.01f)
            {
                _rigidBody.AddForce(transform.up * Force * LeftJoystick.Vertical, ForceMode.Acceleration);
                _verticalSlowDownDiffTime = 0;
            }
            else
            {
                SlowDownVertically();
            }
        }

        private void MoveForward()
        {
            var forwardVelocity = Vector3.Dot(_rigidBody.velocity, transform.forward);
            if (Math.Abs(RightJoystick.Vertical) > 0.01f && Math.Abs(forwardVelocity) < MaxVelocity)
            {
                _rigidBody.AddForce(transform.forward * 8f * RightJoystick.Vertical, ForceMode.Acceleration);
            }
        }

        private void MoveLeftAndRight()
        {
            if (Math.Abs(LeftJoystick.Horizontal) > 0.01f)
            {
                _rigidBody.AddForce(transform.right * 7f * LeftJoystick.Horizontal, ForceMode.Acceleration);
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

        private void SlowDownRotation()
        {
            _rotationSlowDownDiffTime += Time.deltaTime / RotationSlowDownTotalTime;
            var factor = Mathf.Lerp(0, 1, _rotationSlowDownDiffTime);

            var v = _rigidBody.angularVelocity;
            var yCounterVelocity = -v.y * factor;
            _rigidBody.AddTorque(new Vector3(0, yCounterVelocity, 0), ForceMode.VelocityChange);
        }
    }
}   