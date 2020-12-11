using Assets.Scripts.Enemy;
using System;
using UnityEngine;
using Zenject;


public class ChopperInstaller : MonoInstaller
{
    [SerializeField]
    private Settings _settings;

    public override void InstallBindings()
    {
        Container
            .Bind<ChopperPlayer>()
            .AsSingle()
            .WithArguments(_settings.Rigidbody, _settings.ChopperPlayerSettings);

        Container
            .BindInterfacesTo<ChopperFlightHandler>()
            .AsSingle()
            .WithArguments(_settings.ChopperFlightHandlerSettings);

        Container
            .BindInterfacesTo<ChopperRotorsController>()
            .AsSingle()
            .WithArguments(_settings.TopRotorTransform, _settings.RearRotorTransform, _settings.ChopperRotorsControllerSettings);

        Container
            .Bind<ChopperGunShotRangeAreaController>()
            .FromComponentOn(_settings.GunShotAreaRangeGo)
            .AsSingle();


        //***********************


        Container.BindInterfacesTo<EnemySpawner>().AsSingle();
        Container.BindFactory<Enemy, Enemy.Factory>().FromComponentInNewPrefab(_settings.EnemyPrafab);
    }

    [Serializable]
    public class Settings
    {
        public Rigidbody Rigidbody;
        public GameObject GunShotAreaRangeGo;
        public Transform TopRotorTransform;
        public Transform RearRotorTransform;

        public ChopperFlightHandler.Settings ChopperFlightHandlerSettings;
        public ChopperPlayer.Settings ChopperPlayerSettings;
        public ChopperRotorsController.Settings ChopperRotorsControllerSettings;

        public GameObject EnemyPrafab;
    }


    //*************************************


    public class EnemySpawner : ITickable
    {
        readonly Enemy.Factory _enemyFactory;
        readonly ChopperPlayer _player;

        private float _spawnLockPeriod = 5f;
        private float _nextSpawnTime;

        public EnemySpawner(Enemy.Factory enemyFactory, ChopperPlayer player)
        {
            _enemyFactory = enemyFactory;
            _player = player;
        }

        public void Tick()
        {
            if (Time.time > _nextSpawnTime)
            {
                _nextSpawnTime = Time.time + _spawnLockPeriod;

                var enemy = _enemyFactory.Create();
            }
        }
    }
}