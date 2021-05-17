using Assets.Scripts.Entity;

namespace Assets.Scripts.Enemy
{
    public class EnemyManager : EntityManager<Enemy, EnemyParams>
    {
        public EnemyManager(EnemyFactory factory) : base(factory)
        {

        }
    }
}
