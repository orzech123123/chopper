using Assets.Scripts.Effects;
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
        [SerializeField]
        private float _turn = 2f;
        [SerializeField]
        private float _rocketVelocity = 60f;
        [SerializeField]
        private GameObject _smokePrefab;

        private RocketParams _params;
        private Rigidbody _rigidBody;
        private ParticleSystem _smokeParticles;
        private EffectFactories _effectFactories;

        [Inject]
        public void Construct(RocketParams @params, EffectFactories effectFactories)
        {
            transform.position = @params.Position;
            transform.rotation = @params.Rotation;
            _params = @params;
            _effectFactories = effectFactories;
        }

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _smokeParticles = _smokePrefab.GetComponent<ParticleSystem>();
        }

        void FixedUpdate()
        {
            _rigidBody.velocity = transform.forward * _rocketVelocity;

            var rocketTargetRotation = Quaternion.LookRotation(_params.Target.position - transform.position);

            _rigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocketTargetRotation, _turn));
        }

        void OnCollisionEnter(Collision collision)
        {
            _smokeParticles.Stop();
            _smokePrefab.transform.parent = null;
            Destroy(_smokePrefab.gameObject, 5f);

            _effectFactories.ExplosionFactory.Create(new ExplosionParams
            {
                Position = collision.gameObject.transform.position
            });
            _effectFactories.FireFactory.Create(new FireParams
            {
                Position = collision.gameObject.transform.position
            });

            Destroy(gameObject);
        }
    }
}
