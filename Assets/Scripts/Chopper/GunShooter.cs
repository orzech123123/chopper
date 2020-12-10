using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Chopper
{
    //TODO to przerabiam na nie-Monobehaviour:
    //* wstrzyknij skad strzelamy
    //* metoda TryShoot musi odpalac jakis Damage dla Enemy jeśli jakies sa w colliderze
    public class GunShooter : MonoBehaviour
    {
        [SerializeField]
        private Transform _shotStartingMarker;
        [SerializeField]
        private float _shootLockPeriod; 
        private float _nextLaunchTime;

        private ChopperGunShotRangeAreaController _gunShotRangeAreaController;

        [Inject]
        public void Construct(ChopperGunShotRangeAreaController gunShotRangeAreaController)
        {
            _gunShotRangeAreaController = gunShotRangeAreaController;
        }

        public void TryShoot()
        {
            if (Time.time > _nextLaunchTime)
            {
                _nextLaunchTime = Time.time + _shootLockPeriod;

                RaycastHit hitInfo;
                foreach (var enemy in _gunShotRangeAreaController.CollidingObjects.Where(go => go.layer == LayerMask.NameToLayer("Enemy")))
                {
                    var dir = (enemy.transform.position - _shotStartingMarker.position).normalized;
                    if (Physics.Raycast(_shotStartingMarker.position, dir, out hitInfo, float.MaxValue, LayerMask.GetMask("Enemy")))
                    {
                        if (hitInfo.collider.gameObject == enemy)
                        {
                            //var decal = Instantiate(_decalPrefab);
                            //decal.transform.position = hitInfo.point;
                            //decal.transform.forward = hitInfo.normal * -1f;

                            //var line = Instantiate(_drawLinePrefab);
                            //_lines.Add(((int time, GameObject line))(Time.time, line));
                            //var drawer = line.GetComponent<DrawLine>();
                            //drawer.Configure(_shotStartingMarker.position, hitInfo.point);
                            //var renderer = line.GetComponent<LineRenderer>();
                            //renderer.material = new Material(Shader.Find("Mobile/Particles/Additive"));
                            //renderer.startColor = renderer.endColor = Color.red;
                            //renderer.startWidth = renderer.endWidth = 0.1f;

                            GetComponent<RocketLauncher>().TryLaunch(hitInfo.collider.gameObject.transform);
                        }
                    }
                }
            }
        }
    }
}
