using Assets.Scripts.Bullet;
using Assets.Scripts.Rocket;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class WeaponsInstaller : MonoInstaller
    {
        [SerializeField]
        private Settings _settings;

        public override void InstallBindings()
        {
            Container.BindFactory<RocketLauncherParams, Rocket.RocketLauncher, RocketLauncherFactory>();
            Container.BindFactory<RocketParams, Rocket.Rocket, RocketFactory>().FromComponentInNewPrefab(_settings.RocketPrafab);
            Container.BindFactory<BulletParams, Bullet.Bullet, BulletFactory>().FromComponentInNewPrefab(_settings.BulletPrefab);
        }

        [Serializable]
        public class Settings
        {
            public RocketLauncherParams RocketLauncherSettings;
            public GameObject RocketPrafab;
            public GameObject BulletPrefab;
        }
    }
}