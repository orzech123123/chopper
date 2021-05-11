using System;
using UnityEngine;

namespace Assets.Scripts.Rocket
{
    [Serializable]
    public class RocketLauncherSettings
    {
        public float LaunchLockPeriod = 2f;
    }

    public class RocketLauncher
    {
        private readonly RocketFactory factory;
        private readonly RocketLauncherSettings settings;
        private float _nextLaunchTime;

        public RocketLauncher(RocketFactory factory, RocketLauncherSettings settings)
        {
            this.factory = factory;
            this.settings = settings;
        }

        public void Launch(Transform spot, Transform target, LayerMask layer)
        {
            if (Time.time > _nextLaunchTime)
            {
                _nextLaunchTime = Time.time + settings.LaunchLockPeriod;
                factory.Create(new RocketParams
                {
                    Position = spot.position,
                    Rotation = spot.rotation,
                    Target = target,
                    Layer = layer
                });

                ////TODO tests:
                //_rocketFactory.Create(new RocketParams
                //{
                //    Position = new Vector3(0, 100f, 0),
                //    Rotation = Quaternion.identity,
                //    Target = _player.Chopper,
                //    Layer = Layers.EnemyAmmunition
                //});
            }
        }
    }
}
