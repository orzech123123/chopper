using Assets.Scripts.Rocket;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class RocketInstaller : MonoInstaller
    {
        [SerializeField]
        private Settings _settings;

        public override void InstallBindings()
        {
            Container.BindFactory<RocketParams, Rocket.Rocket, RocketFactory>().FromComponentInNewPrefab(_settings.RocketPrafab);
        }

        [Serializable]
        public class Settings
        {
            public GameObject RocketPrafab;
        }
    }
}