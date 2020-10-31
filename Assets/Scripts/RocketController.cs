using System.Collections;
using UnityEngine;

namespace Assets.Scripts
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

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            _rigidBody.velocity = transform.forward * _rocketVelocity;

            var rocketTargetRotation = Quaternion.LookRotation(Target.position - transform.position);

            _rigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocketTargetRotation, _turn));
        }

        void OnCollisionEnter(Collision collision)
        {
            _smoke = transform.Find("WhiteSmoke");
            _smoke.parent = null;
            _smoke.GetComponent<ParticleSystem>().Stop();

            _explosion = transform.Find("ExplosionSpot");
            _explosion.parent = null;
            _explosion.GetComponent<AudioSource>().Play();

            Destroy(gameObject);
            StartCoroutine(Destroy());
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(5f);

            Destroy(_explosion.gameObject);
            Destroy(_smoke.gameObject);
        }
    }
}
