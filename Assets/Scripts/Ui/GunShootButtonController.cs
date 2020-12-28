using Assets.Scripts.Bullet;
using Assets.Scripts.Chopper;
using Assets.Scripts.Rocket;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Scripts.Ui
{
    [RequireComponent(typeof(AudioSource))]
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
        [SerializeField]
        private BulletFactory _bulletFactory;
        [SerializeField]
        private AudioSource _gunAudio;

        void Start()
        {
            _gunAudio = GetComponent<AudioSource>();
        }

        [Inject]
        public void Construct(
            RocketFactory rocketFactory,
            BulletFactory bulletFactory, 
            ChopperPlayer player)
        {
            _rocketFactory = rocketFactory;
            _bulletFactory = bulletFactory;
            _player = player;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _gunAudio.Play();
            _isHeld = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _gunAudio.Stop();
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
                    var enemies = _rangeArea.CollidingObjects.Where(go => go.layer == Layers.Enemy);
                    foreach (var enemy in enemies)
                    {
                        var dir = (enemy.transform.position - _player.Chopper.position).normalized;
                        if (Physics.Raycast(_player.Chopper.position, dir, out hitInfo, float.MaxValue, LayerMask.GetMask(LayerMask.LayerToName(Layers.Enemy))))
                        {
                            if (hitInfo.collider.gameObject == enemy)
                            {
                                var spot = _player.RocketLaunchSpots[Random.Range(0, _player.RocketLaunchSpots.Length)];
                                _bulletFactory.Create(new BulletParams
                                {
                                    Position = spot.position,
                                    Rotation = Quaternion.LookRotation(dir),
                                    Layer = Layers.PlayerAmmunition
                                });
                            } 
                        } 
                    }

                    if(!enemies.Any())
                    {
                        var spot = _player.RocketLaunchSpots[Random.Range(0, _player.RocketLaunchSpots.Length)];
                        _bulletFactory.Create(new BulletParams
                        {
                            Position = spot.position,
                            Rotation = _player.Chopper.rotation,
                            Layer = Layers.PlayerAmmunition
                        });
                    }
                }
            }
        }
    }
}
