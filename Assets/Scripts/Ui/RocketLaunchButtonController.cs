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
        [SerializeField]
        private float _launchLockPeriod;

        private bool _isHeld;
        private RocketFactory _rocketFactory;
        private ChopperPlayer _player;
        private float _nextLaunchTime;

        [Inject]
        public void Construct(RocketFactory rocketFactory, ChopperPlayer player)
        {
            _rocketFactory = rocketFactory;
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
                if (Time.time > _nextLaunchTime)
                {
                    _nextLaunchTime = Time.time + _launchLockPeriod;
                    _rocketFactory.Create(new RocketParams
                    {
                        Position = _player.Position, //TODO change to get launchSpots from here - ChopperPlayer - as from view
                        Rotation = _player.Rotation, //TODO the same as above,
                        Target = _target
                    });
                }
            }
        }
    }
}
