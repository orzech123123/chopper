using UnityEngine;

namespace Assets.Scripts.Rocket
{
    public class RocketLauncher
    {
        private readonly RocketFactory factory;
        [SerializeField]
        private float _launchLockPeriod = 2f;
        private float _nextLaunchTime;

        public RocketLauncher(RocketFactory factory)
        {
            this.factory = factory;
        }

        public void Launch(Transform spot, Transform target, LayerMask layer)
        {
            if (Time.time > _nextLaunchTime)
            {
                _nextLaunchTime = Time.time + _launchLockPeriod;
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
