using Assets.Scripts.Enemy;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField]
        private Settings _settings;

        public override void InstallBindings()
        {
            Container.BindFactory<EnemyParams, Enemy.Enemy, EnemyFactory>().FromComponentInNewPrefab(_settings.EnemyPrafab);
            Container.BindInterfacesAndSelfTo<EnemyManager>().AsSingle();
            Container.BindInterfacesTo<EnemyLifeController>().AsSingle();

            Container.BindInterfacesTo<EnemySpawner>().AsSingle().WithArguments(_settings.SpawnPoint);
        }

        [Serializable]
        public class Settings
        {
            public GameObject EnemyPrafab;
            public Transform SpawnPoint;
        }
    }
}