using Assets.Scripts.Chopper;
using System;
using UnityEngine;
using Zenject;


public class ChopperInstaller : MonoInstaller
{
    [SerializeField]
    private Settings _settings;

    public override void InstallBindings()
    {
        Container.Bind<ChopperPlayer>().AsSingle().WithArguments(_settings.Rigidbody);

        Container.BindInterfacesTo<ChopperFlightHandler>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public Rigidbody Rigidbody;
    }
}