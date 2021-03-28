using Assets.Scripts.Chopper;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class EnemySpawner : ITickable
    {
        private EnemyManager _enemyManager;
        private Transform _spawnPoint;
        private float _spawnLockPeriod = 10f;
        private float _nextSpawnTime;

        public EnemySpawner(EnemyManager enemyManager, Transform spawnPoint)
        {
            _enemyManager = enemyManager;
            _spawnPoint = spawnPoint;
        }

        public void Tick()
        {
            if (Time.time > _nextSpawnTime)
            {
                _nextSpawnTime = Time.time + _spawnLockPeriod;

                _enemyManager.Create(new EnemyParams { Position = new Vector3(_spawnPoint.position.x, 5f, _spawnPoint.position.z) });
            }
        }
    }
}
