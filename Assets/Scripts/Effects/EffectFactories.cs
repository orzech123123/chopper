using Zenject;

namespace Assets.Scripts.Effects
{
    public class FireFactory : PlaceholderFactory<FireParams, Fire>
    {
    }

    public class ExplosionFactory : PlaceholderFactory<ExplosionParams, Explosion>
    {
    }

    public class EffectFactories
    {
        public EffectFactories(FireFactory fireFactory, ExplosionFactory explosionFactory)
        {
            FireFactory = fireFactory;
            ExplosionFactory = explosionFactory;
        }

        public FireFactory FireFactory { get; }
        public ExplosionFactory ExplosionFactory { get; }
    }
}
