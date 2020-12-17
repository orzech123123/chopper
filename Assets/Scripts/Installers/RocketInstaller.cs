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
        Container.BindFactory<RocketSettings, Rocket, RocketFactory>().FromComponentInNewPrefab(_settings.RocketPrafab);
    }

    [Serializable]
    public class Settings
    {
        public GameObject RocketPrafab;
    }
}