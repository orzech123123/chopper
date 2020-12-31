using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeArea : MonoBehaviour
{
    private List<GameObject> _collidingObjects = new List<GameObject>();
    public IEnumerable<GameObject> CollidingObjects => _collidingObjects;

    void OnTriggerEnter(Collider other)
    {
        //TODO to moze nie dzialac dobrze jak obiekt bedzie zagniezdzony (Enemy sa w pierwszym poziomie root wiec dziala!!!)
        if(!_collidingObjects.Contains(other.transform.root.gameObject))
        {
            _collidingObjects.Add(other.transform.root.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        _collidingObjects.Remove(other.transform.root.gameObject);
    }

    private void Update()
    {
        _collidingObjects = _collidingObjects.Where(co => co != null).ToList();
    }
}
