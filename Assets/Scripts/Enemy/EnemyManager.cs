using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class EnemyManager : ITickable
    {
        readonly IList<Enemy> _enemies = new List<Enemy>();

        public EnemyManager()
        {
        }

        public void Add(Enemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void Tick()
        {
            Debug.Log(_enemies.Count);
        }
    }
}
