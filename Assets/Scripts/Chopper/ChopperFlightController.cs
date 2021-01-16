using Assets.Scripts.Input;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Chopper
{
    [Serializable]
    public class ChopperFlightControllerSettings
    {
        public float MaxVelocity = 10f;
    }

    public class ChopperFlightController : IFixedTickable
    {
        readonly ChopperPlayer _chopperPlayer;
        readonly IInputManager _inputManager;
        readonly ChopperFlightControllerSettings _settings;

        readonly float _targetXAngle = 20;

        public ChopperFlightController(
            ChopperPlayer chopperPlayer,
            IInputManager inputManager,
            ChopperFlightControllerSettings settings)
        {
            _chopperPlayer = chopperPlayer;
            _inputManager = inputManager;
            _settings = settings;
        }

        public void FixedTick()
        {
            if (_inputManager.IsForwardActive && Math.Abs(_chopperPlayer.ForwardVelocity) < _settings.MaxVelocity)
            {
                _chopperPlayer.MoveForward(_inputManager.ForwardValue);
            }
            else
            {
                _chopperPlayer.SlowDownForward();
            }


            if (Math.Abs(_inputManager.VerticalValue) > 0/*_inputManager.IsVerticalActive && Math.Abs(_chopperPlayer.UpVelocity) < _settings.MaxVelocity*/)
            {
                //TODO zrobic obracanie wg smiglowca, a nie osi X bo jak jest obrocony to nie dziala dobrze!!!
                // var _targetRotation = Quaternion.AngleAxis(20 * _inputManager.VerticalValue, _chopperPlayer.transform.right) * _chopperPlayer.transform.rotation;
                //var _targetRotation = Quaternion.Euler(new Vector3(20 * _inputManager.VerticalValue, _chopperPlayer.transform.rotation.y, _chopperPlayer.transform.rotation.z) ) * _chopperPlayer.transform.right;
                //Quaternion localRotation =   Quaternion.Euler(20 * _inputManager.VerticalValue, _chopperPlayer.transform.localRotation.eulerAngles.y, _chopperPlayer.transform.localRotation.eulerAngles.z);
                // _chopperPlayer.transform.rotation = _chopperPlayer.transform.rotation * localRotation;
                // _chopperPlayer.transform.rotation = Quaternion.RotateTowards(_chopperPlayer.transform.rotation, _targetRotation, 15f * Time.deltaTime);
                //_chopperPlayer.MoveVertically(_inputManager.VerticalValue);

                //if(Math.Abs(_chopperPlayer.transform.localRotation.eulerAngles.x) < 20f)
                //{
                //    _chopperPlayer.transform.RotateAround(_chopperPlayer.transform.position, _chopperPlayer.transform.right, Time.deltaTime * 90f * _inputManager.VerticalValue);
                //}
                //Debug.Log("kk " + _chopperPlayer.transform.localRotation.eulerAngles.x);

                //var _targetRotation = Quaternion.Euler(new Vector3(20 * _inputManager.VerticalValue, _chopperPlayer.transform.rotation.y, _chopperPlayer.transform.rotation.z));
                //_chopperPlayer.transform.rotation = Quaternion.RotateTowards(_chopperPlayer.transform.rotation, _targetRotation, 15f * Time.deltaTime);
                //

                Quaternion rotationRight = Quaternion.AngleAxis(30 * _inputManager.VerticalValue/*UnityEngine.Input.acceleration.z*/ * Time.deltaTime, Vector3.right);
                Quaternion targetRotation = _chopperPlayer.transform.rotation * rotationRight;

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
                if(Math.Abs(rotX) <= 25)
                {
                    _chopperPlayer.transform.rotation = targetRotation;
                }
            }
            else
            {
                _chopperPlayer.SlowDownVertically();
            }
            _chopperPlayer.SlowDownVertically();


            if (_inputManager.IsLeftRightActive && Math.Abs(_chopperPlayer.LeftRightVelocity) < _settings.MaxVelocity)
            {
                _chopperPlayer.MoveLeftRight(_inputManager.LeftRightValue);
            }
            else
            {
                _chopperPlayer.SlowDownLeftRight();
            }

            if (_inputManager.IsTurnActive)
            {
                _chopperPlayer.RotateOnYAxis(_inputManager.TurnValue);
            }
            else
            {
                _chopperPlayer.SlowDownRotation();
            }
        }
    }
}