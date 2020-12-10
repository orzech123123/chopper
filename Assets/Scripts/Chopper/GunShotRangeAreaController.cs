using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotRangeAreaController : MonoBehaviour
{
    private ICollection<GameObject> _collidingObjects = new List<GameObject>();
    public IEnumerable<GameObject> CollidingObjects => _collidingObjects;

    void OnTriggerEnter(Collider other)
    {
        if(!_collidingObjects.Contains(other.gameObject))
        {
            _collidingObjects.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        _collidingObjects.Remove(other.gameObject);
    }
}
