using UnityEngine;

namespace Assets.Scripts
{
    public class RocketLauncher : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private Transform[] _launchSpots;
        [SerializeField]
        private GameObject _rocketPrefab;

        void Update()
        {
            if (Input.GetMouseButtonDown(0) &&
                Input.mousePosition.x > Screen.width / 2f &&
                Input.mousePosition.y > Screen.height / 2f)
            {
                var launchSpot = _launchSpots[Random.Range(0, _launchSpots.Length - 1)];

                var rocketGo = Instantiate(_rocketPrefab, launchSpot.position, transform.rotation);
                rocketGo.GetComponent<RocketController>().Target = _target;
            }
        }
    }
}
