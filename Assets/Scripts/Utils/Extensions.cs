using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class Extensions
    {
        public static GameObject GetRoot(this Collider collider)
        {
            //var x = PrefabUtility.GetOutermostPrefabInstanceRoot(collider.gameObject);
            //return x;

            //TODO ULEPSZ TO bo to działa przez przypadek
            return collider.gameObject.transform.root.gameObject;
        }

    }
}
