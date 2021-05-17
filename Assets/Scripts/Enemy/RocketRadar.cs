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

    public class RocketRadar : MonoBehaviour, IDamagable
    {
        [SerializeField]
        private int _totalHealth = 500;
        [SerializeField]
        private float _rocketTurn = 0.6f;

        public int CurrentHealth { get; private set; }
        public int TotalHealth => _totalHealth;

        public RocketLauncher _rocketLauncher;
        private ChopperPlayer _player;

        [SerializeField]
        private Transform[] _rocketLaunchSpots;

        [Inject]
        public void Construct([InjectOptional] RocketRadarParams @params, ChopperPlayer player, RocketLauncherFactory rocketLauncherFactory)
        {
            CurrentHealth = _totalHealth;
            _rocketLauncher = rocketLauncherFactory.Create(new RocketLauncherParams
            {
                RocketExplosionWithFire = false,
                RocketTurn = _rocketTurn
            });
            transform.position = @params?.Position ?? transform.position;
            _player = player;
        }

        void Update() 
        {
            var spot = _rocketLaunchSpots[Random.Range(0, _rocketLaunchSpots.Length)];
            _rocketLauncher.Launch(spot, _player.Chopper, Layers.EnemyAmmunition);
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
        }
    }
}
