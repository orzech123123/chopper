using Assets.Scripts.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 
 
                    _enemy = _rangeArea.CollidingObjects.Contains(_enemy) ?
                        _enemy :
                        _rangeArea.CollidingObjects
                            .GroupBy(co => new
                            {
                                co,
                                distance = Vector3.Distance(_player.Chopper.position, co.transform.position)
                            })
                            .OrderBy(co => co.Key.distance)
                            .Select(co => co.Key.co)
                            .FirstOrDefault(e => e.layer == Layers.Enemy);

                    if (_enemy != null)
                    {
                        RaycastHit hitInfo;
                        var dir = (_enemy.transform.position - _player.Chopper.position).normalized;
                        if (Physics.Raycast(_player.Chopper.position, dir, out hitInfo, float.MaxValue, LayerMask.GetMask(LayerMask.LayerToName(Layers.Enemy))))
                        {
                            if (hitInfo.collider.GetRoot() == _enemy)
                            {
                                _bulletFactory.Create(new BulletParams
                                {
                                    Position = _player.Chopper.position,
                                    Rotation = Quaternion.LookRotation(dir),
                                    Layer = Layers.PlayerAmmunition
                                });

                                var damagable = (IDamagable)_enemy.GetComponent(typeof(IDamagable));
                                damagable?.TakeDamage(5);
                            }
                        }
                    }
                    else
                    {
                        _bulletFactory.Create(new BulletParams
                        {
                            Position = _player.Chopper.position,
                            Rotation = _player.Chopper.rotation,
                            Layer = Layers.PlayerAmmunition
                        });
                    }
 
 */


public class RangeArea : MonoBehaviour
{
    private List<GameObject> _collidingObjects = new List<GameObject>();
    public IEnumerable<GameObject> CollidingObjects => _collidingObjects;

    void OnTriggerEnter(Collider other)
    {
        if(!_collidingObjects.Contains(other.GetRoot()))
        {
            _collidingObjects.Add(other.GetRoot());
        }
    }

    void OnTriggerExit(Collider other)
    {
        _collidingObjects.Remove(other.GetRoot());
    }

    private void Update()
    {
        _collidingObjects = _collidingObjects.Where(co => co != null).ToList();
    }
}
