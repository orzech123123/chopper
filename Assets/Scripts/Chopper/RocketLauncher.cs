using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Chopper
{
    //TODO to przerabiam na nie-Monobehaviour:
    //* wstrzyknij skad strzelamy (lunchSpoty)
    //* wstrzyknij RocketSpawner
    //* metoda TryLunch musi odpalac jakis RocketSpawner z parametrami...
    //* zrobic celowanie i wybor celu na jakiejs podstawie
    public class RocketLauncher : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _launchSpots;
        [SerializeField]
        private GameObject _rocketPrefab;
        [SerializeField]
        private float _launchLockPeriod; 

        private float _nextLaunchTime;
        private Rocket.Rocket.Factory _rocketFactory;

        [Inject]
        public void Construct(Rocket.Rocket.Factory rocketFactory)
        {
            _rocketFactory = rocketFactory;
        }

        public void TryLaunch(Transform target)
        {
            if (Time.time > _nextLaunchTime)
            {
                _nextLaunchTime = Time.time + _launchLockPeriod;

                var launchSpot = _launchSpots[Random.Range(0, _launchSpots.Length - 1)];

                _rocketFactory.Create(new Rocket.Rocket.Settings
                {
                    Position = launchSpot.position,
                    Rotation = transform.rotation,
                    Target = target
                });
            }
        }
    }
}
