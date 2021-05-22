using System;

namespace Assets.Scripts.Input
{
    public class PcInputManager : IInputManager
    {
        private Joystick _leftJoystick;
        private Joystick _rightJoystick;

        private float _inertnessValue = 0.3f;

        public PcInputManager(Joystick leftJoystick, Joystick rightJoystick)
        {
            _leftJoystick = leftJoystick;
            _rightJoystick = rightJoystick;
        }

        public float LeftRightValue => _leftJoystick.Horizontal;

        public float ForwardValue => _rightJoystick.Vertical;

        public float TurnOnXValue => _leftJoystick.Vertical;

        public float TurnOnYValue => _rightJoystick.Horizontal;

        public bool IsLeftRightActive => IsNotInert(LeftRightValue);

        public bool IsForwardActive => IsNotInert(ForwardValue);

        public bool IsTurnOnXActive => IsNotInert(TurnOnXValue);

        public bool IsTurnOnYActive => IsNotInert(TurnOnYValue);

        public float VerticalValue => _leftJoystick.Vertical;

        public bool IsVerticalActive => IsNotInert(VerticalValue);

        private bool IsNotInert(float value) => Math.Abs(value) > _inertnessValue;
    }
}
