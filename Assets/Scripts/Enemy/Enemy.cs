﻿using Assets.Scripts.Chopper;
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
        [SerializeField]
        private int _totalHealth = 100;

        public int CurrentHealth { get; private set; }
        public int TotalHealth => _totalHealth;

        private NavMeshAgent _agent;
        private ChopperPlayer _player;

        [Inject]
        public void Construct([InjectOptional] EnemyParams @params, ChopperPlayer player)
        {
            CurrentHealth = _totalHealth;
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
            CurrentHealth -= damage;
        }

        void Update()
        {
            if(CurrentHealth > 0)
            {
                _agent.SetDestination(new Vector3(_player.Chopper.position.x, 0, _player.Chopper.position.z));
            }
        }
    }
}
