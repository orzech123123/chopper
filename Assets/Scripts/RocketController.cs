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
            Destroy(gameObject);
        }
    }
}
