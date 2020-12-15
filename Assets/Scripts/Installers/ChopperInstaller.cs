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
    }
}