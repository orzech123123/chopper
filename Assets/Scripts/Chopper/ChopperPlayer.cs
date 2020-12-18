using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Chopper
{
    [Serializable]
    public class ChopperPlayerParams
    {
    }

    [RequireComponent(typeof(Rigidbody))]
    public class ChopperPlayer : MonoBehaviour
    {
        [SerializeField]
        private float _maxVerticalForce = 25f;
        [SerializeField]
        private float _slowDownTotalTime = 5f;
        [SerializeField]
        private Transform _topRotor;
        [SerializeField]
        private Transform _rearRotor;
        [SerializeField]
        private Transform _helicopter;
        [SerializeField]
        private Transform[] _rocketLaunchSpots;

        private Rigidbody _rigidbody;
        private float _rotationSlowDownDiffTime;
        private float _leftRightSlowDownDiffTime;
        private float _forwardSlowDownDiffTime;
        private float _verticalSlowDownDiffTime;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public float UpVelocity => Vector3.Dot(_rigidbody.velocity, _rigidbody.transform.up);
        public float ForwardVelocity => Vector3.Dot(_rigidbody.velocity, _rigidbody.transform.forward);
        public float LeftRightVelocity => Vector3.Dot(_rigidbody.velocity, _rigidbody.transform.right);

        public Transform[] RocketLaunchSpots => _rocketLaunchSpots;
        public Vector3 Position => _helicopter.position;
        public Quaternion Rotation => _helicopter.rotation;

        public void RotateOnYAxis(float powerFactor)
        {
            _rigidbody.AddTorque(_rigidbody.transform.up * 0.2f * powerFactor, ForceMode.Acceleration);
            _rotationSlowDownDiffTime = 0f;
        }

        public void MoveVertically(float powerFactor)
        {
            _rigidbody.AddForce(_rigidbody.transform.up * _maxVerticalForce * powerFactor, ForceMode.Acceleration);
            _verticalSlowDownDiffTime = 0f;
        }

        public void MoveForward(float powerFactor)
        {
            _rigidbody.AddForce(_rigidbody.transform.forward * 8f * powerFactor, ForceMode.Acceleration);
            _forwardSlowDownDiffTime = 0f;
        }

        public void MoveLeftRight(float powerFactor)
        {
            _rigidbody.AddForce(_rigidbody.transform.right * 7f * powerFactor, ForceMode.Acceleration);
            _leftRightSlowDownDiffTime = 0f;
        }

        public void SlowDownLeftRight()
        {
            _leftRightSlowDownDiffTime += Time.deltaTime / _slowDownTotalTime;
            var factor = Mathf.Lerp(0, 1, _leftRightSlowDownDiffTime);

            var rightVelocity = Vector3.Dot(_rigidbody.velocity, _rigidbody.transform.right);
            var counterRightVelocity = -rightVelocity * factor;
            _rigidbody.AddForce(_rigidbody.transform.right * counterRightVelocity, ForceMode.VelocityChange);
        }

        public void SlowDownVertically()
        {
            _verticalSlowDownDiffTime += Time.deltaTime / _slowDownTotalTime;
            var factor = Mathf.Lerp(0, 1, _verticalSlowDownDiffTime);

            var upVelocity = Vector3.Dot(_rigidbody.velocity, _rigidbody.transform.up);
            var counterUpVelocity = -upVelocity * factor;
            _rigidbody.AddForce(_rigidbody.transform.up * counterUpVelocity, ForceMode.VelocityChange);
        }

        public void SlowDownForward()
        {
            _forwardSlowDownDiffTime += Time.deltaTime / _slowDownTotalTime;
            var factor = Mathf.Lerp(0, 1, _forwardSlowDownDiffTime);

            var forwardVelocity = Vector3.Dot(_rigidbody.velocity, _rigidbody.transform.forward);
            var counterForwardVelocity = -forwardVelocity * factor;
            _rigidbody.AddForce(_rigidbody.transform.forward * counterForwardVelocity, ForceMode.VelocityChange);
        }

        public void SlowDownRotation()
        {
            _rotationSlowDownDiffTime += Time.deltaTime / _slowDownTotalTime;
            var factor = Mathf.Lerp(0, 1, _rotationSlowDownDiffTime);

            var v = _rigidbody.angularVelocity;
            var yCounterVelocity = -v.y * factor;
            _rigidbody.AddTorque(new Vector3(0, yCounterVelocity, 0), ForceMode.VelocityChange);
        }

        public void SetTopRotorSpeed(float speed)
        {
            _topRotor.localRotation = Quaternion.Euler(0, speed, 0) * _topRotor.localRotation;
        }

        public void SetRearRotorSpeed(float speed)
        {
            _rearRotor.localRotation = Quaternion.Euler(speed, 0, 0) * _rearRotor.localRotation;
        }
    }
}