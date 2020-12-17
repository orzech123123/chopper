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
        Container.BindFactory<EnemyParams, Enemy, EnemyFactory>().FromComponentInNewPrefab(_settings.EnemyPrafab);
        Container.BindInterfacesAndSelfTo<EnemyManager>().AsSingle();

        Container.BindInterfacesTo<EnemySpawner>().AsSingle(); //TODO remove
    }

    [Serializable]
    public class Settings
    {
        public GameObject EnemyPrafab;
    }
}