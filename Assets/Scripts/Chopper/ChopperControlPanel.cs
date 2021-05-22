using Assets.Scripts.Input;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Chopper
{
    public class ChopperControlPanel : ControlPanel
    {
        //[SerializeField]
        //private Joystick _leftJoystick;
        //[SerializeField]
        //private Joystick _rightJoystick;

        private IInputManager _inputManager;

        [Inject]
        public void Construct(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

        void FixedUpdate()
        {
            var pressedKeyCode = new List<PressedKeyCode>();

            if (_inputManager.IsVerticalActive)
            {
                if (_inputManager.VerticalValue > 0)
                {
                    pressedKeyCode.Add(PressedKeyCode.SpeedUpPressed);
                }
                else
                {
                    pressedKeyCode.Add(PressedKeyCode.SpeedDownPressed);
                }
            }

            if (_inputManager.IsForwardActive)
            {
                if (_inputManager.ForwardValue > 0)
                {
                    pressedKeyCode.Add(PressedKeyCode.ForwardPressed);
                }
                else
                {
                    pressedKeyCode.Add(PressedKeyCode.BackPressed);
                }
            }

            if (_inputManager.IsLeftRightActive)
            {
                if (_inputManager.LeftRightValue > 0)
                {
                    pressedKeyCode.Add(PressedKeyCode.RightPressed);
                }
                else
                {
                    pressedKeyCode.Add(PressedKeyCode.LeftPressed);
                }
            }

            if (_inputManager.IsTurnOnYActive)
            {
                if (_inputManager.TurnOnYValue > 0)
                {
                    pressedKeyCode.Add(PressedKeyCode.TurnRightPressed);
                }
                else
                {
                    pressedKeyCode.Add(PressedKeyCode.TurnLeftPressed);
                }
            }

            KeyPressed(pressedKeyCode.ToArray());
        }
    }
}
