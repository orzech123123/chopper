using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Effects
{
    [Serializable]
    public class FireParams
    {
        public Vector3 Position;
    }

    public class Fire : MonoBehaviour
    {
        [Inject]
        public void Construct(FireParams @params)
        {
            transform.position = @params.Position;
        }
    }
}
