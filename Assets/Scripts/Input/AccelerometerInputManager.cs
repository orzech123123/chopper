using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Input
{
    public class AccelerometerInputManager : IInputManager, IInitializable, ITickable
    {
        private Joystick _leftJoystick;
        private Joystick _rightJoystick;

        Vector3 zeroAc;
        Vector3 curAc;
        float sensH = 5f;
        float sensV = 5f;
        float smooth = 1f;
        float GetAxisH = 0;
        float GetAxisV = 0;
        private float _inertnessValue = 0.01f;

        public AccelerometerInputManager(Joystick leftJoystick, Joystick rightJoystick)
        {
            _leftJoystick = leftJoystick;
            _rightJoystick = rightJoystick;
        }

        public float LeftRightValue => _leftJoystick.Horizontal;

        public float ForwardValue => _rightJoystick.Vertical;

        public float VerticalValue 
        { 
            get
            {
                return GetAxisV;
            } 
        }

        public float TurnValue //=> _rightJoystick.Horizontal;
        {
            get
            {
                return GetAxisH;
            }
        }


        public bool IsLeftRightActive => IsNotInert(LeftRightValue);

        public bool IsForwardActive => IsNotInert(ForwardValue);

        public bool IsVerticalActive => IsNotInert(VerticalValue);

        public bool IsTurnActive => IsNotInert(TurnValue);

        public void Initialize()
        {
            zeroAc = UnityEngine.Input.acceleration;
            curAc = Vector3.zero;
        }

        public void Tick()
        {
            curAc = Vector3.Lerp(curAc, UnityEngine.Input.acceleration - zeroAc, Time.deltaTime / smooth);
            GetAxisV = Mathf.Clamp(curAc.y * sensV, -1, 1);
            GetAxisH = Mathf.Clamp(curAc.x * sensH, -1, 1);
            Debug.Log("Krajne (V-H): " + GetAxisV + ":" + GetAxisH);
        }

        private bool IsNotInert(float value) => Math.Abs(value) > _inertnessValue;
    }
}
