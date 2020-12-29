using Assets.Scripts.Chopper;
using Assets.Scripts.Interfaces;
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
    public class Enemy : MonoBehaviour, IInitializable, IDamagable
    {
        private NavMeshAgent _agent; 
        private ChopperPlayer _player;

        [SerializeField]
        private int _health = 100;

        public bool IsFullyDamaged => _health <= 0;

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

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }

        void Update()
        {
            //TODO!!
            if(!IsFullyDamaged)
            {
                _agent.SetDestination(new Vector3(_player.Chopper.position.x, 0, _player.Chopper.position.z));
            }
            else
            {
                if(GetComponent<NavMeshAgent>() != null)
                {
                    Destroy(GetComponent<NavMeshAgent>());
                }
            }
        }
    }
}
