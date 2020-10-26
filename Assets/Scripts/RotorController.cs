using UnityEngine;

namespace Assets.Scripts
{
    public class RotorController : MonoBehaviour
    {
        [SerializeField]
        private int _speed = 500;

        void Update()
        {
            var rotor = transform.Find("Sk_Veh_Attack_Heli/Rotor");
            var rotor2 = transform.Find("Sk_Veh_Attack_Heli/Rotor2");

            rotor.localRotation = Quaternion.Euler(0, _speed * Time.deltaTime, 0) * rotor.localRotation;
            rotor2.localRotation = Quaternion.Euler(_speed * Time.deltaTime, 0, 0) * rotor2.localRotation;
        }
    }
}
