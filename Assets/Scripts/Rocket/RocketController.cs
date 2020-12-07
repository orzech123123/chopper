using UnityEngine;

namespace Assets.Scripts.Rocket
{
    public class RocketController : MonoBehaviour
    {
        public Transform Target;
        private Rigidbody _rigidBody;
        [SerializeField]
        private float _turn;
        [SerializeField]
        private float _rocketVelocity;
        private Transform _smoke;
        private Transform _explosion;
        [SerializeField]
        private GameObject _firePrefab;
        [SerializeField]
        private GameObject _explosionPrefab;

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _smoke = transform.Find("WhiteSmoke");
            _explosion = transform.Find("ExplosionSpot");
        }

        void FixedUpdate()
        {
            _rigidBody.velocity = transform.forward * _rocketVelocity;

            var rocketTargetRotation = Quaternion.LookRotation(Target.position - transform.position);

            _rigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocketTargetRotation, _turn));
        }

        void OnCollisionEnter(Collision collision)
        {
            _smoke.parent = null;
            _explosion.parent = null;

            _smoke.GetComponent<ParticleSystem>().Stop();
            _explosion.GetComponent<AudioSource>().Play();

            Destroy(gameObject);
            Destroy(_smoke.gameObject, 5f);
            Destroy(_explosion.gameObject, 5f);

            Instantiate(_firePrefab, collision.gameObject.transform.position, _firePrefab.transform.rotation);
            Instantiate(_explosionPrefab, collision.gameObject.transform.position, _explosionPrefab.transform.rotation);
        }
    }
}
