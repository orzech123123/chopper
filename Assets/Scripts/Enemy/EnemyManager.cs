using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class EnemyManager : ITickable
    {
        readonly IList<Enemy> _enemies = new List<Enemy>();
        readonly EnemyFactory _factory;

        public EnemyManager(EnemyFactory factory)
        {
            _factory = factory;
        }

        public void Create(EnemySettings settings)
        {
            var enemy = _factory.Create(settings);
            _enemies.Add(enemy);
        }

        public void Tick()
        {
            Debug.Log(_enemies.Count);
        }
    }
}
