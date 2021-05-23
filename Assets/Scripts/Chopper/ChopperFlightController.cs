using Assets.Scripts.Input;
using System;
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
         
        }
    }
}