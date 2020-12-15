using Assets.Scripts.Rocket;
using System;
using UnityEngine;
using Zenject;


public class RocketInstaller : MonoInstaller
{
    [SerializeField]
    private Settings _settings;

    public override void InstallBindings()
    {
        //Container.BindInterfacesAndSelfTo<EnemyManager>().AsSingle();
        //Container.BindInterfacesTo<EnemySpawner>().AsSingle();
        Container.BindFactory<Rocket.Settings, Rocket, Rocket.Factory>().FromComponentInNewPrefab(_settings.RocketPrafab);
    }

    [Serializable]
    public class Settings
    {
        public GameObject RocketPrafab;
    }
}