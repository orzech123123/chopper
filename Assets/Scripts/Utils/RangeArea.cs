using Assets.Scripts.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
