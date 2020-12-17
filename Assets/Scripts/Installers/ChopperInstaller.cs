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
            .BindInterfacesTo<ChopperFlightController>()
            .AsSingle()
            .WithArguments(_settings.ChopperFlightControllerSettings);

        Container
            .BindInterfacesTo<ChopperRotorsController>()
            .AsSingle()
            .WithArguments(_settings.ChopperRotorsControllerSettings);
    }

    [Serializable]
    public class Settings
    {
        public ChopperFlightControllerSettings ChopperFlightControllerSettings;
        public ChopperRotorsControllerSettings ChopperRotorsControllerSettings;
    }
}