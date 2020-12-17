using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [Inject]
        public void Construct(EnemySettings settings)
        {
            transform.position = settings.Position;
        }
    }
}
