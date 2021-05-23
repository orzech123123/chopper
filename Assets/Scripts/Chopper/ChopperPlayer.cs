using System;
using UnityEngine;

namespace Assets.Scripts.Chopper
{
    [Serializable]
    public class ChopperPlayerParams
    {
    }

    public class ChopperPlayer : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _rocketLaunchSpots;

        public Transform[] RocketLaunchSpots => _rocketLaunchSpots;
        public Transform Chopper => transform;
    }
}