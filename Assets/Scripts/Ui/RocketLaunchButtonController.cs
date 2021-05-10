using Assets.Scripts.Chopper;
using Assets.Scripts.Rocket;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Scripts.Ui
{
    public class RocketLaunchButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, ITickable
    {
        [SerializeField]
        private Transform _target;

        private bool _isHeld;
        private RocketLauncher _rocketLauncher;
        private ChopperPlayer _player;

        [Inject]
        public void Construct(RocketLauncher rocketLauncher, ChopperPlayer player)
        {
            _rocketLauncher = rocketLauncher;
            _player = player;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isHeld = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isHeld = false;
        }

        public void Tick()
        {
            if (_isHeld)
            {
                var spot = _player.RocketLaunchSpots[Random.Range(0, _player.RocketLaunchSpots.Length)];
                _rocketLauncher.Launch(spot, _target, Layers.PlayerAmmunition);   
            }
        }
    }
}
