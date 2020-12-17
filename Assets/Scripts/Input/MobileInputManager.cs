using System;

namespace Assets.Scripts.Input
{
    public class MobileInputManager : IInputManager
    {
        private Joystick _leftJoystick;
        private Joystick _rightJoystick;

        private float _inertnessValue = 0.01f;

        public MobileInputManager(Joystick leftJoystick, Joystick rightJoystick)
        {
            _leftJoystick = leftJoystick;
            _rightJoystick = rightJoystick;
        }

        public float LeftRightValue => _leftJoystick.Horizontal;

        public float ForwardValue => _rightJoystick.Vertical;

        public float VerticalValue => _leftJoystick.Vertical;

        public float TurnValue => _rightJoystick.Horizontal;

        public bool IsLeftRightActive => IsNotInert(LeftRightValue);

        public bool IsForwardActive => IsNotInert(ForwardValue);

        public bool IsVerticalActive => IsNotInert(VerticalValue);

        public bool IsTurnActive => IsNotInert(TurnValue);

        private bool IsNotInert(float value) => Math.Abs(value) > _inertnessValue;
    }
}
