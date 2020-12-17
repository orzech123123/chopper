using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class EnemySpawner : ITickable
    {
        EnemyManager _enemyManager;
        ChopperPlayer _player;

        private float _spawnLockPeriod = 5f;
        private float _nextSpawnTime;

        public EnemySpawner(EnemyManager enemyManager, ChopperPlayer player)
        {
            _enemyManager = enemyManager;
            _player = player;
        }

        public void Tick()
        {
            if (Time.time > _nextSpawnTime)
            {
                _nextSpawnTime = Time.time + _spawnLockPeriod;

                _enemyManager.Create(new EnemySettings { Position = new Vector3(_player.Position.x, 5f, _player.Position.z) });
            }
        }
    }
}
