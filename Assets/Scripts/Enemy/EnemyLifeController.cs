using Assets.Scripts.Effects;
using System.Linq;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class EnemyLifeController : ITickable
    {
        private readonly EnemyManager _enemyManager;
        private readonly EffectFactories _effectFactories;

        public EnemyLifeController(EnemyManager enemyManager, EffectFactories effectFactories)
        {
            _enemyManager = enemyManager;
            _effectFactories = effectFactories;
        }

        public void Tick()
        {
            foreach (var enemy in _enemyManager.Enemies.Where(e => e.CurrentHealth < 0).ToList())
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
