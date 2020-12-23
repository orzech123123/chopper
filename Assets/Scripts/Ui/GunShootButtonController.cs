using Assets.Scripts.Chopper;
using Assets.Scripts.Rocket;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Scripts.Ui
{
    public class GunShootButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, ITickable
    {
        private bool _isHeld;
        private RocketFactory _rocketFactory;
        private ChopperPlayer _player;
        private float _nextShotTime;

        [SerializeField]
        private RangeArea _rangeArea;
        [SerializeField]
        private float _shotLockPeriod;

        [Inject]
        public void Construct(
            RocketFactory rocketFactory,
            ChopperPlayer player)
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
                if (Time.time > _nextShotTime)
                {
                    _nextShotTime = Time.time + _shotLockPeriod;

                    RaycastHit hitInfo;
                    foreach (var enemy in _rangeArea.CollidingObjects.Where(go => go.layer == Layers.Enemy))
                    {
                        var dir = (enemy.transform.position - _player.Chopper.position).normalized;
                        if (Physics.Raycast(_player.Chopper.position, dir, out hitInfo, float.MaxValue, LayerMask.GetMask(LayerMask.LayerToName(Layers.Enemy))))
                        {
                            if (hitInfo.collider.gameObject == enemy)
                            {
                                var spot = _player.RocketLaunchSpots[Random.Range(0, _player.RocketLaunchSpots.Length)];
                                _rocketFactory.Create(new RocketParams
                                {
                                    Position = spot.position,
                                    Rotation = spot.rotation, 
                                    Target = enemy.transform,
                                    Layer = Layers.PlayerAmmunition
                                });
                            } 
                        } 
                    }
                }
            }
        }
    }
}
