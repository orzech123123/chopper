using Assets.Scripts.Effects;
using Assets.Scripts.Interfaces;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Bullet
{
    [Serializable]
    public class BulletParams
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public LayerMask Layer;
    }

    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float _bulletVelocity = 250f;
        [SerializeField]
        private float _lifetimeSeconds = 2f;

        private BulletParams _params;
        private float _startTime;

        [Inject]
        public void Construct(BulletParams @params)
        {
            transform.position = @params.Position;
            transform.rotation = @params.Rotation;
            gameObject.layer = @params.Layer;
            _params = @params;
            _startTime = Time.time;
        }

        void Start()
        {
        }

        void FixedUpdate()
        {
            transform.position += transform.forward * _bulletVelocity * Time.deltaTime;
        }

        private void Update()
        {
            if(Time.time > _startTime + _lifetimeSeconds)
            {
                Destroy(gameObject);
            }
        }
    }
}
