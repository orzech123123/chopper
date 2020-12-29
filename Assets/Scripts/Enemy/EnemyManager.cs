﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class EnemyManager : ITickable
    {
        readonly List<Enemy> _enemies = new List<Enemy>();
        readonly EnemyFactory _factory;

        public EnemyManager(EnemyFactory factory)
        {
            _factory = factory;
            _enemies.AddRange(Object.FindObjectsOfType<Enemy>());
        }

        public void Create(EnemyParams @params)
        {
            var enemy = _factory.Create(@params);
            _enemies.Add(enemy);
        }

        public void Tick()
        {
            Debug.Log($"Healthy: {_enemies.Count(e => !e.IsFullyDamaged)} | Damaged: {_enemies.Count(e => e.IsFullyDamaged)}");
        }
    }
}
