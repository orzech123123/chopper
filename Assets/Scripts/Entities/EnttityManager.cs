using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Entity
{
    public class EntityManager<TEntity, TEntityParams> where TEntity : MonoBehaviour
    {
        [Inject]
        protected DiContainer Container { get; set; }

        readonly List<TEntity> _entities = new List<TEntity>();
        readonly EntityFactory<TEntity, TEntityParams> _factory;

        public EntityManager(EntityFactory<TEntity, TEntityParams> factory)
        {
            _factory = factory;
            _entities.AddRange(Object.FindObjectsOfType<TEntity>());
        }

        public void Create(TEntityParams @params)
        {
            var enemy = _factory.Create(@params);
            _entities.Add(enemy);
        }

        public void Destroy(TEntity entity)
        {
            _entities.Remove(entity);
            Object.Destroy(entity.gameObject);
        }

        public IEnumerable<TEntity> Entities => _entities;
    }
}
