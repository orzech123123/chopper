using Assets.Scripts.Effects;
using System.Linq;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class RocketRadarLifeController : ITickable
    {
        private readonly RocketRadarManager _manager;
        private readonly EffectFactories _effectFactories;

        public RocketRadarLifeController(RocketRadarManager manager, EffectFactories effectFactories)
        {
            _manager = manager;
            _effectFactories = effectFactories;
        }

        public void Tick()
        {
            foreach (var entity in _manager.Entities.Where(e => e.CurrentHealth < 0).ToList())
            {
                _manager.Destroy(entity);
                _effectFactories.ExplosionFactory.Create(new ExplosionParams
                {
                    Position = entity.transform.position
                });
            }
        }
    }
}
