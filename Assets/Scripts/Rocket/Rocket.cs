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
        private float _startTime;

        [Inject]
        public void Construct(RocketParams @params, EffectFactories effectFactories)
        {
            transform.position = @params.Position;
            transform.rotation = @params.Rotation;
            _params = @params;
            _effectFactories = effectFactories;
            gameObject.layer = @params.Layer;
            _startTime = Time.time;
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

            if(Time.time > _startTime + 5f)
            {
                DestroyWithEffects();
            }
        }

        private void DestroyWithEffects()
        {
            RunEffects();
            Destroy(gameObject);
        }

        void OnTriggerEnter(Collider collider)
        {
            var other = collider.gameObject;
            if (other.layer != _params.Target.gameObject.layer)
            {
                return;
            }

            DestroyWithEffects();
        }

        private void RunEffects()
        {
            _smokeParticles.Stop();
            _smokePrefab.transform.parent = null;
            Destroy(_smokePrefab.gameObject, 5f);

            _effectFactories.ExplosionFactory.Create(new ExplosionParams
            {
                Position = transform.position
            });

            if(!_params.ExplosionWithFire)
            {
                return;
            }

            _effectFactories.FireFactory.Create(new FireParams
            {
                Position = transform.position
            });
        }
    }
}
