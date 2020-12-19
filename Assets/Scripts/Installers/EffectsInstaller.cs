using Assets.Scripts.Effects;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class EffectsInstaller : MonoInstaller
    {
        [SerializeField]
        private Settings _settings;

        public override void InstallBindings()
        {
            Container.BindFactory<FireParams, Fire, FireFactory>().FromComponentInNewPrefab(_settings.FirePrefab);
            Container.BindFactory<ExplosionParams, Explosion, ExplosionFactory>().FromComponentInNewPrefab(_settings.ExplosionPrefab);
            Container.Bind<EffectFactories>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public GameObject FirePrefab;
            public GameObject ExplosionPrefab;
        }
    }
}