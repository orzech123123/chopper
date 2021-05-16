using Assets.Scripts.Interfaces;
using Zenject;
using UnityEngine;
using Assets.Scripts.Chopper;
using Assets.Scripts.Rocket;

namespace Assets.Scripts.Enemy
{
    public class RocketRadarParams
    {
        public Vector3 Position;
    }

    public class RocketRadar : MonoBehaviour, ITickable, IDamagable
    {
        [SerializeField]
        private int _totalHealth = 100;
        private int _currentHealth;
        public int CurrentHealth => _currentHealth;
        public int TotalHealth => _totalHealth;

        public RocketLauncher _rocketLauncher;
        private ChopperPlayer _player;

        [SerializeField]
        private Transform[] _rocketLaunchSpots;

        [Inject]
        public void Construct([InjectOptional] RocketRadarParams @params, ChopperPlayer player, RocketLauncherFactory rocketLauncherFactory)
        {
            _currentHealth = _totalHealth;
            _rocketLauncher = rocketLauncherFactory.Create(new RocketLauncherParams
            {
                RocketExplosionWithFire = false
            });
            transform.position = @params?.Position ?? transform.position;
            _player = player;
        }

        public void Tick() //TODO move this to RocketRadarLaunchControllera czy coś
        {
            var spot = _rocketLaunchSpots[Random.Range(0, _rocketLaunchSpots.Length)];
            _rocketLauncher.Launch(spot, _player.Chopper, Layers.EnemyAmmunition);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
        }
    }
}
