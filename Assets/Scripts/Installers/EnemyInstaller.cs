using Assets.Scripts.Enemy;
using System;
using UnityEngine;
using Zenject;


public class EnemyInstaller : MonoInstaller
{
    [SerializeField]
    private Settings _settings;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<EnemyManager>().AsSingle();
        Container.BindInterfacesTo<EnemySpawner>().AsSingle();
        Container.BindFactory<Enemy, Enemy.Factory>().FromComponentInNewPrefab(_settings.EnemyPrafab);
    }

    [Serializable]
    public class Settings
    {
        public GameObject EnemyPrafab;
    }
}