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
                            //TODO shootingh
                        }
                    }
                }
            }
        }
    }
}
