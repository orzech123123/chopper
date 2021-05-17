using Assets.Scripts.Entity;

namespace Assets.Scripts.Enemy
{
    public class RocketRadarManager : EntityManager<RocketRadar, RocketRadarParams>
    {
        public RocketRadarManager(RocketRadarFactory factory) : base(factory)
        {

        }
    }
}
