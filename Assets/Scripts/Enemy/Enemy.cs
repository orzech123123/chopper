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
            transform.position = new Vector3(player.Position.x, 5f, player.Position.z);
        }

        public class Factory : PlaceholderFactory<Enemy>
        {
        }

        public void Update()
        {
        }
    }
}
