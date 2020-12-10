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
    }

    [Serializable]
    public class Settings
    {
        public Rigidbody Rigidbody;
        public ChopperFlightHandler.Settings ChopperFlightHandlerSettings;
        public ChopperPlayer.Settings ChopperPlayerSettings;
    }
}