using Assets.Scripts.Input;
using System;
using Zenject;

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


        if (_inputManager.IsVerticalActive && Math.Abs(_chopperPlayer.UpVelocity) < _settings.MaxVelocity)
        {
            _chopperPlayer.MoveVertically(_inputManager.VerticalValue);
        }
        else
        {
            _chopperPlayer.SlowDownVertically();
        }

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