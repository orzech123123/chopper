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
        private RangeArea _rangeArea;
        private RocketFactory _rocketFactory;
        private ChopperPlayer _player;
        private float _nextShotTime;

        [SerializeField]
        private float _shotLockPeriod;

        [Inject]
        public void Construct(
            RangeArea rangeArea,
            RocketFactory rocketFactory,
            ChopperPlayer player)
        {
            _rangeArea = rangeArea;
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
                    foreach (var enemy in _rangeArea.CollidingObjects.Where(go => go.layer == LayerMask.NameToLayer("Enemy")))
                    {
                        var dir = (enemy.transform.position - _player.Position).normalized;
                        if (Physics.Raycast(_player.Position, dir, out hitInfo, float.MaxValue, LayerMask.GetMask("Enemy")))
                        {
                            if (hitInfo.collider.gameObject == enemy)
                            {
                                _rocketFactory.Create(new RocketParams
                                {
                                    Position = _player.Position, //TODO change to get launchSpots from here - ChopperPlayer - as from view
                                    Rotation = _player.Rotation, //TODO the same as above,
                                    Target = enemy.transform
                                });
                            } 
                        } 
                    }
                }
            }
        }
    }
}
