using UnityEngine;

namespace Assets.Scripts.Chopper
{
    public class RotorController : MonoBehaviour
    {
        public float MaxSpeed = 500f;

        public float EnginesWarmUpTotalTime = 10f;
        private float _enginesWarmUpDiffTime;

        void Update()
        {
            var rotor = transform.Find("Sk_Veh_Attack_Heli/Rotor");
            var rotor2 = transform.Find("Sk_Veh_Attack_Heli/Rotor2");

            _enginesWarmUpDiffTime += Time.deltaTime / EnginesWarmUpTotalTime;
            var factor = Mathf.Lerp(0, 1, _enginesWarmUpDiffTime);
            var speed = MaxSpeed * factor;

            rotor.localRotation = Quaternion.Euler(0, speed, 0) * rotor.localRotation;
            rotor2.localRotation = Quaternion.Euler(speed, 0, 0) * rotor2.localRotation;
        }
    }
}
