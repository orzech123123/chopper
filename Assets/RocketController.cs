using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Rigidbody _rigidBody;
    [SerializeField]
    private float _turn;
    [SerializeField]
    private float _rocketVelocity;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        _rigidBody.velocity = transform.forward * _rocketVelocity;

        var rocketTargetRotation = Quaternion.LookRotation(_target.position - transform.position);

        _rigidBody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocketTargetRotation, _turn));
    }
}
