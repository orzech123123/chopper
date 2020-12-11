using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        ChopperPlayer _player;

        [Inject]
        public void Construct(ChopperPlayer player)
        {
            _player = player;
            transform.position = player.Position;
        }

        public class Factory : PlaceholderFactory<Enemy>
        {
        }

        public void Update()
        {
            Debug.Log(_player.ForwardVelocity);
        }
    }
}
