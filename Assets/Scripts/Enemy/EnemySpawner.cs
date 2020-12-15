using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class EnemySpawner : ITickable
    {
        readonly Enemy.Factory _enemyFactory;
        EnemyManager _enemyManager;

        private float _spawnLockPeriod = 5f;
        private float _nextSpawnTime;

        public EnemySpawner(Enemy.Factory enemyFactory, EnemyManager enemyManager)
        {
            _enemyFactory = enemyFactory;
            _enemyManager = enemyManager;
        }

        public void Tick()
        {
            if (Time.time > _nextSpawnTime)
            {
                _nextSpawnTime = Time.time + _spawnLockPeriod;

                var enemy = _enemyFactory.Create();
                _enemyManager.Add(enemy);
            }
        }
    }
}
