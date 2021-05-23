using Assets.Scripts.Input;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Chopper
{
    public class ChopperControlPanel : ControlPanel
    {
        private IInputManager _inputManager;
        private HelicopterController _helicopterController;

        [Inject]
        public void Construct(IInputManager inputManager, HelicopterController helicopterController)
        {
            _inputManager = inputManager;
            _helicopterController = helicopterController;
        }

        private void LateUpdate()
        {
            if(_helicopterController.IsOnGround)
            {
                return;
            }

            _helicopterController.hMove = new Vector2(_inputManager.LeftRightValue, _inputManager.ForwardValue);
            _helicopterController.turnValueFromInputManager = _inputManager.TurnOnYValue;
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

            if(!_helicopterController.IsOnGround)
            {
                pressedKeyCode.Add(PressedKeyCode.TurnLeftPressed);
            }

            KeyPressed(pressedKeyCode.ToArray());
        }
    }
}
