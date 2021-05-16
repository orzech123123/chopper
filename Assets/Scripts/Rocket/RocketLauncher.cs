using System;
using UnityEngine;

namespace Assets.Scripts.Rocket
{
    [Serializable]
    public class RocketLauncherParams
    {
        public float LaunchLockPeriod = 2f;
        public bool RocketExplosionWithFire = true;
    }

    public class RocketLauncher
    {
        private readonly RocketFactory factory;
        private readonly RocketLauncherParams @params;
        private float _nextLaunchTime;

        public RocketLauncher(RocketFactory factory, RocketLauncherParams @params)
        {
            this.factory = factory;
            this.@params = @params;
        }

        public void Launch(Transform spot, Transform target, LayerMask layer)
        {
            if (Time.time > _nextLaunchTime)
            {
                _nextLaunchTime = Time.time + @params.LaunchLockPeriod;
                factory.Create(new RocketParams
                {
                    Position = spot.position,
                    Rotation = spot.rotation,
                    Target = target,
                    Layer = layer,
                    ExplosionWithFire = @params.RocketExplosionWithFire
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
