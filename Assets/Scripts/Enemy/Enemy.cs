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
    public class Enemy : MonoBehaviour
    {
        private NavMeshAgent _agent; 
        private ChopperPlayer _player; 

        [Inject]
        public void Construct(EnemyParams @params, ChopperPlayer player)
        {
            transform.position = @params.Position;
            _player = player;
        }

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _agent.SetDestination(new Vector3(_player.Chopper.position.x, 0, _player.Chopper.position.z));
        }
    }
}
