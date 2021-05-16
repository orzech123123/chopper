using Assets.Scripts.Effects;
using Assets.Scripts.Interfaces;
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
        public LayerMask Layer;
        public Transform Target;
        public bool ExplosionWithFire;
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
            gameObject.layer = @params.Layer;
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

        void OnTriggerEnter(Collider collider)
        {
            var other = collider.gameObject;
            if (other.layer != _params.Target.gameObject.layer)
            {
                return;
            }

            RunEffects(other);

            Destroy(gameObject);
        }

        private void RunEffects(GameObject other)
        {
            _smokeParticles.Stop();
            _smokePrefab.transform.parent = null;
            Destroy(_smokePrefab.gameObject, 5f);

            _effectFactories.ExplosionFactory.Create(new ExplosionParams
            {
                Position = other.transform.position
            });

            if(!_params.ExplosionWithFire)
            {
                return;
            }

            _effectFactories.FireFactory.Create(new FireParams
            {
                Position = other.transform.position
            });
        }
    }
}
