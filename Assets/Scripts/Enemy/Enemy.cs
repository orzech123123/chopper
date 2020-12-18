using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class EnemyParams
    {
        public Vector3 Position;
    }

    [RequireComponent(typeof(Rigidbody))]
    public class Enemy : MonoBehaviour
    {
        [Inject]
        public void Construct(EnemyParams @params)
        {
            transform.position = @params.Position;
        }
    }
}
