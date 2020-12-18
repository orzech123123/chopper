using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Rocket
{
    [Serializable]
    public class RocketParams
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Transform Target;
    }

    [RequireComponent(typeof(Rigidbody))]
    public class Rocket : MonoBehaviour
    {
        //TODO dac to z jakiegos facotry ognia i eksplozji
        [SerializeField]
        private GameObject _firePrefab;
        [SerializeField]
        private GameObject _explosionPrefab;

        [SerializeField]
        private float _turn = 2f;
        [SerializeField]
        private float _rocketVelocity = 60f;
        [SerializeField]
        private GameObject _smokePrefab;

        private RocketParams _params;
        private Rigidbody _rigidBody;

        [Inject]
        public void Construct(RocketParams @params)
        {
            transform.position = @params.Position;
            transform.rotation = @params.Rotation;
            _params = @params;
        }

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            _rigidBody.velocity = transform.forward * _rocketVelocity;

            var rocketTargetRotation = Quaternion.LookRotation(_params.Target.position - transform.position);

            _rigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocketTargetRotation, _turn));
        }

        void OnCollisionEnter(Collision collision)
        {
            _smokePrefab.GetComponent<ParticleSystem>().Stop();
            _smokePrefab.transform.parent = null;
            
            //TODO przeniesc explozje do jakiegos factory z eksplozjami
            var explosion = transform.Find("ExplosionSpot");
            explosion.parent = null;
            explosion.GetComponent<AudioSource>().Play();

            Destroy(gameObject);
            Destroy(_smokePrefab.gameObject, 5f);
            Destroy(explosion.gameObject, 5f);

            //TODO to inicjowac z jakiegos factory
            Instantiate(_firePrefab, collision.gameObject.transform.position, _firePrefab.transform.rotation);
            Instantiate(_explosionPrefab, collision.gameObject.transform.position, _explosionPrefab.transform.rotation);
        }
    }
}
