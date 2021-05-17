using UnityEngine;
using Zenject;

namespace Assets.Scripts.Entity
{
    public class EntityFactory<TEntity, TEntityParams> : PlaceholderFactory<TEntityParams, TEntity> where TEntity : MonoBehaviour
    {
    }
}
