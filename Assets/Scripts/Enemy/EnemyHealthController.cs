﻿using Assets.Scripts.Effects;
using System.Linq;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class EnemyHealthController : ITickable
    {
        private readonly EnemyManager _enemyManager;
        private readonly EffectFactories _effectFactories;

        public EnemyHealthController(EnemyManager enemyManager, EffectFactories effectFactories)
        {
            _enemyManager = enemyManager;
            _effectFactories = effectFactories;
        }

        public void Tick()
        {
            foreach (var enemy in _enemyManager.Enemies.Where(e => e.CurrentHealth < 0))
            {
                _enemyManager.Destroy(enemy);
                _effectFactories.ExplosionFactory.Create(new ExplosionParams
                {
                    Position = enemy.transform.position
                });
            }
        }
    }
}
