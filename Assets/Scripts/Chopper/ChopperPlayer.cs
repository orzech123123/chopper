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

        public void MoveVertically(float verticalValue)
        {
            _rigidbody.AddForce(transform.up * 15f * verticalValue, ForceMode.Acceleration);
            _verticalSlowDownDiffTime = 0;
        }

        public float LeftRightVelocity => Vector3.Dot(_rigidbody.velocity, _rigidbody.transform.right);

        public Transform[] RocketLaunchSpots => _rocketLaunchSpots;
        public Transform Chopper => _helicopter.transform;

        public void RotateOnYAxis(float powerFactor)
        {
            //TODO zmien to

            //var angles = _rigidbody.transform.localEulerAngles;
            //_rigidbody.transform.localEulerAngles = new Vector3(0, angles.y, 0);
            //_rigidbody.AddTorque(Vector3.up * 0.2f * powerFactor, ForceMode.Acceleration);
            //_rigidbody.transform.localEulerAngles = new Vector3(angles.x, _rigidbody.transform.localEulerAngles.y, angles.z);
            //TODO zrob to na AddTorque
            _rigidbody.transform.Rotate(0, powerFactor * 0.6f, 0, Space.World);

            _rotationSlowDownDiffTime = 0f;
        }

        public void RotateOnXAxis(float powerFactor)
        {
            //TODO zmien to
            Quaternion rotationRight = Quaternion.AngleAxis(30 * powerFactor * Time.deltaTime, Vector3.right);
            Quaternion targetRotation = _rigidbody.transform.rotation * rotationRight;

            var rotX = targetRotation.eulerAngles.x;
            if (rotX > 180)
            {
                rotX -= 360;
            }
            else if (rotX < -180)
            {
                rotX += 360;
            }

            Debug.Log("rr " + rotX);
            if (Math.Abs(rotX) <= 25)
            {
                _rigidbody.transform.rotation = targetRotation;
            }
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

        public void SlowDownYRotation()
        {
            _rotationSlowDownDiffTime += Time.deltaTime / _slowDownTotalTime;
            var factor = Mathf.Lerp(0, 1, _rotationSlowDownDiffTime);

            var v = _rigidbody.angularVelocity;
            var counterVelocity = -v * factor;
            _rigidbody.AddTorque(counterVelocity, ForceMode.VelocityChange);

            //TODO
            //1 slow down rotation of Chopper object (root) when hit by sth to return it into proper rotation
            //2 rotate Heli mesh inside root with rotation when speed increases/decreases
            
            //ad1 done slowing down after collision - also need to lerp to 0, y, 0 rotation in some time
            return;
            Vector3 faceTarget = new Vector3(0, _rigidbody.rotation.y, 0);
            Quaternion currentRotation = _rigidbody.rotation;
            Quaternion newRotation = Quaternion.LookRotation(faceTarget);
            _rigidbody.MoveRotation(Quaternion.Slerp(currentRotation, newRotation, 2f));
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