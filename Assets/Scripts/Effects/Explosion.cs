using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Effects
{
    [Serializable]
    public class ExplosionParams
    {
        public Vector3 Position;
    }

    public class Explosion : MonoBehaviour
    {
        private AudioSource _audio;

        [Inject]
        public void Construct(ExplosionParams @params)
        {
            transform.position = @params.Position;
        }

        void Start()
        {
            _audio = GetComponent<AudioSource>();
            _audio.Play();
            Destroy(gameObject, 5f);
        }
    }
}
