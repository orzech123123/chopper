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
                    var spot = _player.RocketLaunchSpots[Random.Range(0, _player.RocketLaunchSpots.Length)];
                    _rocketFactory.Create(new RocketParams
                    {
                        Position = spot.position, 
                        Rotation = spot.rotation, 
                        Target = _target,
                        Layer = LayerMask.NameToLayer("PlayerAmmunition")
                    });

                    //TODO tests:
                    _rocketFactory.Create(new RocketParams
                    {
                        Position = new Vector3(0, 100f, 0),
                        Rotation = Quaternion.identity,
                        Target = _player.Chopper,
                        Layer = LayerMask.NameToLayer("EnemyAmmunition")
                    });
                }
            }
        }
    }
}
