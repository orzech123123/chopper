using Assets.Scripts.Bullet;
using Assets.Scripts.Chopper;
using Assets.Scripts.Enemy;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Rocket;
using Assets.Scripts.Utils;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Ui
{
    [RequireComponent(typeof(AudioSource))]
    public class GunShootButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, ITickable
    {
        private bool _isHeld;
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
        [SerializeField]
        private UnityEngine.Camera _camera;
        [SerializeField]
        private Image _aimImage;

        private GameObject _enemy;

        void Start()
        {
            _gunAudio = GetComponent<AudioSource>();
        }

        [Inject]
        public void Construct(
            BulletFactory bulletFactory,
            ChopperPlayer player)
        {
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



                    Ray ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);

                    var position = new Vector2(Screen.width * _aimImage.rectTransform.anchorMin.x + _aimImage.rectTransform.anchoredPosition.x,
                                               Screen.height * _aimImage.rectTransform.anchorMin.y + _aimImage.rectTransform.anchoredPosition.y);

                    RaycastHit hit;
                    Ray r = _camera.ScreenPointToRay(position);
                    if (Physics.Raycast(r, out hit, float.MaxValue, LayerMask.GetMask(LayerMask.LayerToName(Layers.Enemy)) | LayerMask.GetMask(LayerMask.LayerToName(Layers.Terrain))))
                    {
                        var dir = (hit.point - _player.Chopper.position).normalized;

                        if(hit.collider.gameObject.layer == Layers.Enemy)
                        {
                            var damagable = (IDamagable)hit.rigidbody.GetComponent(typeof(IDamagable));
                            damagable?.TakeDamage(5);
                        }

                        _bulletFactory.Create(new BulletParams
                        {
                            Position = _player.Chopper.position,
                            Rotation = Quaternion.LookRotation(dir),
                            Layer = Layers.PlayerAmmunition
                        });
                    }

                    return;

                    _enemy = _rangeArea.CollidingObjects.Contains(_enemy) ?
                        _enemy :
                        _rangeArea.CollidingObjects
                            .GroupBy(co => new
                            {
                                co,
                                distance = Vector3.Distance(_player.Chopper.position, co.transform.position)
                            })
                            .OrderBy(co => co.Key.distance)
                            .Select(co => co.Key.co)
                            .FirstOrDefault(e => e.layer == Layers.Enemy);

                    if (_enemy != null)
                    {
                        RaycastHit hitInfo;
                        var dir = (_enemy.transform.position - _player.Chopper.position).normalized;
                        if (Physics.Raycast(_player.Chopper.position, dir, out hitInfo, float.MaxValue, LayerMask.GetMask(LayerMask.LayerToName(Layers.Enemy))))
                        {
                            if (hitInfo.collider.GetRoot() == _enemy)
                            {
                                _bulletFactory.Create(new BulletParams
                                {
                                    Position = _player.Chopper.position,
                                    Rotation = Quaternion.LookRotation(dir),
                                    Layer = Layers.PlayerAmmunition
                                });

                                var damagable = (IDamagable)_enemy.GetComponent(typeof(IDamagable));
                                damagable?.TakeDamage(5);
                            }
                        }
                    }
                    else
                    {
                        _bulletFactory.Create(new BulletParams
                        {
                            Position = _player.Chopper.position,
                            Rotation = _player.Chopper.rotation,
                            Layer = Layers.PlayerAmmunition
                        });
                    }
                }
            }
        }
    }
}
