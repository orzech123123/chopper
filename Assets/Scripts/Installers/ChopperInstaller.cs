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
            .BindInterfacesTo<ChopperFlightHandler>()
            .AsSingle()
            .WithArguments(_settings.ChopperFlightHandlerSettings);

        Container
            .BindInterfacesTo<ChopperRotorsController>()
            .AsSingle()
            .WithArguments(_settings.TopRotorTransform, _settings.RearRotorTransform, _settings.ChopperRotorsControllerSettings);
    }

    [Serializable]
    public class Settings
    {
        public GameObject GunShotAreaRangeGo;
        public Transform TopRotorTransform;
        public Transform RearRotorTransform;

        public ChopperFlightHandler.Settings ChopperFlightHandlerSettings;
        public ChopperPlayerParams ChopperPlayerParams;
        public ChopperRotorsController.Settings ChopperRotorsControllerSettings;
    }
}