using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotorController : MonoBehaviour
{
    [SerializeField]
    private int _speed = 500;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rotor = transform.Find("Sk_Veh_Attack_Heli/Rotor");
        var rotor2 = transform.Find("Sk_Veh_Attack_Heli/Rotor2");

        rotor.localRotation = Quaternion.Euler(0, _speed * Time.deltaTime, 0) * rotor.localRotation;
        rotor2.localRotation = Quaternion.Euler(_speed * Time.deltaTime, 0, 0) * rotor2.localRotation;
    }
}
