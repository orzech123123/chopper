using UnityEngine;
using Zenject;

namespace Assets.Scripts.Rocket
{
    public class Rocket : MonoBehaviour
    {
        private Transform _target;
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

        [Inject]
        public void Construct(Settings settings)
        {
            transform.position = settings.Position;
            transform.rotation = settings.Rotation;
            _target = settings.Target;
        }

        public class Factory : PlaceholderFactory<Settings, Rocket>
        {
        }

        public class Settings
        {
            public Vector3 Position;
            public Quaternion Rotation;
            public Transform Target;
        }

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _smoke = transform.Find("WhiteSmoke");
            _explosion = transform.Find("ExplosionSpot");
        }

        void FixedUpdate()
        {
            _rigidBody.velocity = transform.forward * _rocketVelocity;

            var rocketTargetRotation = Quaternion.LookRotation(_target.position - transform.position);

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
