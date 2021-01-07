using System;
using UnityEngine;

namespace Assets.Scripts.Input
{
    public class AccelerometerInputManager : IInputManager
    {
        private Joystick _leftJoystick;
        private Joystick _rightJoystick;

        private float _inertnessValue = 0.01f;

        private Vector3 _initAccelerometerPosition;
        private float _accelerometerMaxAnglesDiff = 0.3f;

        public AccelerometerInputManager(Joystick leftJoystick, Joystick rightJoystick)
        {
            _leftJoystick = leftJoystick;
            _rightJoystick = rightJoystick;
            _initAccelerometerPosition = UnityEngine.Input.acceleration;
            Debug.Log("upsztr " + _initAccelerometerPosition);
        }

        public float LeftRightValue => _leftJoystick.Horizontal;

        public float ForwardValue => _rightJoystick.Vertical;

        public float VerticalValue 
        { 
            get
            {
                var min = _initAccelerometerPosition.y - _accelerometerMaxAnglesDiff;
                var max = _initAccelerometerPosition.y + _accelerometerMaxAnglesDiff;
                var value = Mathf.Clamp(min, max, UnityEngine.Input.acceleration.y);
                value /= _accelerometerMaxAnglesDiff;
                return value;
            } 
        }

        public float TurnValue => _rightJoystick.Horizontal;

        public bool IsLeftRightActive => IsNotInert(LeftRightValue);

        public bool IsForwardActive => IsNotInert(ForwardValue);

        public bool IsVerticalActive => IsNotInert(VerticalValue);

        public bool IsTurnActive => IsNotInert(TurnValue);

        private bool IsNotInert(float value) => Math.Abs(value) > _inertnessValue;
    }
}
