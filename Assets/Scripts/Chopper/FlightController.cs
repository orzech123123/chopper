using Assets.Scripts.Input;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Scripts.Chopper
{
    [RequireComponent(typeof(Rigidbody))]
    public class FlightController : MonoBehaviour
    {
        private IInputManager _inputManager;

        [Inject]
        public void Init(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

        public float MaxAngularVelocity = 10f;
        public float MaxVelocity = 10f;
        public float Force = 25f;

        private Rigidbody _rigidBody;

        public float VerticalSlowDownTotalTime = 5f;
        private float _verticalSlowDownDiffTime;

        public float ForwardSlowDownTotalTime = 5f;
        private float _forwardSlowDownDiffTime;

        public float RotationSlowDownTotalTime = 5f;
        private float _rotationSlowDownDiffTime;

        public float LeftRightSlowDownTotalTime = 5f;
        private float _leftRightSlowDownDiffTime;

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _rigidBody.maxAngularVelocity = MaxAngularVelocity;
        }

        void FixedUpdate()
        {
            MoveVertically();

            MoveLeftRight();

            MoveForward();

            RotateOnYAxis();
        }

        private void RotateOnYAxis()
        {
            if (_inputManager.IsTurnActive)
            {
                _rigidBody.AddTorque(transform.up * 0.2f * _inputManager.TurnValue, ForceMode.Acceleration);
                _rotationSlowDownDiffTime = 0;
            }
            else
            {
                SlowDownRotation();
            }
        }

        private void MoveVertically()
        {
            var upVelocity = Vector3.Dot(_rigidBody.velocity, transform.up);
            if (_inputManager.IsVerticalActive && Math.Abs(upVelocity) < MaxVelocity)
            {
                _rigidBody.AddForce(transform.up * Force * _inputManager.VerticalValue, ForceMode.Acceleration);
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
            if (_inputManager.IsForwardActive && Math.Abs(forwardVelocity) < MaxVelocity)
            {
                _rigidBody.AddForce(transform.forward * 8f * _inputManager.ForwardValue, ForceMode.Acceleration);
                _forwardSlowDownDiffTime = 0f;
            }
            else
            {
                SlowDownForward();
            }
        }

        private void MoveLeftRight()
        {
            var rightVelocity = Vector3.Dot(_rigidBody.velocity, transform.right);
            if (_inputManager.IsLeftRightActive && Math.Abs(rightVelocity) < MaxVelocity)
            {
                _rigidBody.AddForce(transform.right * 7f * _inputManager.LeftRightValue, ForceMode.Acceleration);
                _leftRightSlowDownDiffTime = 0f;
            }
            else
            {
                SlowDownLeftRight();
            }
        }

        private void SlowDownLeftRight()
        {
            _leftRightSlowDownDiffTime += Time.deltaTime / LeftRightSlowDownTotalTime;
            var factor = Mathf.Lerp(0, 1, _leftRightSlowDownDiffTime);

            var rightVelocity = Vector3.Dot(_rigidBody.velocity, transform.right);
            var counterRightVelocity = -rightVelocity * factor;
            _rigidBody.AddForce(transform.right * counterRightVelocity, ForceMode.VelocityChange);
        }

        private void SlowDownVertically()
        {
            _verticalSlowDownDiffTime += Time.deltaTime / VerticalSlowDownTotalTime;
            var factor = Mathf.Lerp(0, 1, _verticalSlowDownDiffTime);

            var upVelocity = Vector3.Dot(_rigidBody.velocity, transform.up);
            var counterUpVelocity = -upVelocity * factor;
            _rigidBody.AddForce(transform.up * counterUpVelocity, ForceMode.VelocityChange);
        }

        private void SlowDownForward()
        {
            _forwardSlowDownDiffTime += Time.deltaTime / ForwardSlowDownTotalTime;
            var factor = Mathf.Lerp(0, 1, _forwardSlowDownDiffTime);

            var forwardVelocity = Vector3.Dot(_rigidBody.velocity, transform.forward);
            var counterForwardVelocity = -forwardVelocity * factor;
            _rigidBody.AddForce(transform.forward * counterForwardVelocity, ForceMode.VelocityChange);
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