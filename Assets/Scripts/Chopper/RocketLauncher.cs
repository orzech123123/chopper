using System.Linq;
using Assets.Scripts.Rocket;
using UnityEngine;

namespace Assets.Scripts.Chopper
{
    public class RocketLauncher : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _launchSpots;
        [SerializeField]
        private GameObject _rocketPrefab;
        [SerializeField]
        private float _launchLockPeriod; 
        private float _nextLaunchTime;

        public void TryLaunch(Transform target)
        {
            if (Time.time > _nextLaunchTime)
            {
                _nextLaunchTime = Time.time + _launchLockPeriod;

                var launchSpot = _launchSpots[Random.Range(0, _launchSpots.Length - 1)];

                var rocketGo = Instantiate(_rocketPrefab, launchSpot.position, transform.rotation);
                rocketGo.GetComponent<RocketController>().Target = target;


                //TODO remove
                var light = transform.Find("RedLightSphere");
                light.gameObject.SetActive(!light.gameObject.activeInHierarchy);
            }
        }
    }
}
