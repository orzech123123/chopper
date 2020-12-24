using Assets.Scripts.Chopper;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class EnemyParams
    {
        public Vector3 Position;
    }

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour, IInitializable
    {
        private NavMeshAgent _agent; 
        private ChopperPlayer _player;

        [Inject]
        public void Construct([InjectOptional] EnemyParams @params, ChopperPlayer player)
        {
            transform.position = @params?.Position ?? transform.position;
            _player = player;
        }

        [Inject]
        public void Initialize()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            _agent.SetDestination(new Vector3(_player.Chopper.position.x, 0, _player.Chopper.position.z));
        }
    }
}
