using System;
using UnityEngine;

[Serializable]
public class ChopperPlayerParams
{
    public float MaxVerticalForce = 25f;
    public float SlowDownTotalTime = 5f;
}

public class ChopperPlayer : MonoBehaviour
{
    [SerializeField]
    private ChopperPlayerParams _settings;

    private Rigidbody _rigidbody;
    private float _rotationSlowDownDiffTime;
    private float _leftRightSlowDownDiffTime;
    private float _forwardSlowDownDiffTime;
    private float _verticalSlowDownDiffTime;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public float UpVelocity => Vector3.Dot(_rigidbody.velocity, _rigidbody.transform.up);
    public float ForwardVelocity => Vector3.Dot(_rigidbody.velocity, _rigidbody.transform.forward);
    public float LeftRightVelocity => Vector3.Dot(_rigidbody.velocity, _rigidbody.transform.right);

    public void RotateOnYAxis(float powerFactor)
    {
        _rigidbody.AddTorque(_rigidbody.transform.up * 0.2f * powerFactor, ForceMode.Acceleration);
        _rotationSlowDownDiffTime = 0f;
    }

    public void MoveVertically(float powerFactor)
    {
        _rigidbody.AddForce(_rigidbody.transform.up * _settings.MaxVerticalForce * powerFactor, ForceMode.Acceleration);
        _verticalSlowDownDiffTime = 0f;
    }

    public void MoveForward(float powerFactor)
    {
        _rigidbody.AddForce(_rigidbody.transform.forward * 8f * powerFactor, ForceMode.Acceleration);
        _forwardSlowDownDiffTime = 0f;
    }

    public void MoveLeftRight(float powerFactor)
    {
        _rigidbody.AddForce(_rigidbody.transform.right * 7f * powerFactor, ForceMode.Acceleration);
        _leftRightSlowDownDiffTime = 0f;
    }

    public void SlowDownLeftRight()
    {
        _leftRightSlowDownDiffTime += Time.deltaTime / _settings.SlowDownTotalTime;
        var factor = Mathf.Lerp(0, 1, _leftRightSlowDownDiffTime);

        var rightVelocity = Vector3.Dot(_rigidbody.velocity, _rigidbody.transform.right);
        var counterRightVelocity = -rightVelocity * factor;
        _rigidbody.AddForce(_rigidbody.transform.right * counterRightVelocity, ForceMode.VelocityChange);
    }

    public void SlowDownVertically()
    {
        _verticalSlowDownDiffTime += Time.deltaTime / _settings.SlowDownTotalTime;
        var factor = Mathf.Lerp(0, 1, _verticalSlowDownDiffTime);

        var upVelocity = Vector3.Dot(_rigidbody.velocity, _rigidbody.transform.up);
        var counterUpVelocity = -upVelocity * factor;
        _rigidbody.AddForce(_rigidbody.transform.up * counterUpVelocity, ForceMode.VelocityChange);
    }

    public void SlowDownForward()
    {
        _forwardSlowDownDiffTime += Time.deltaTime / _settings.SlowDownTotalTime;
        var factor = Mathf.Lerp(0, 1, _forwardSlowDownDiffTime);

        var forwardVelocity = Vector3.Dot(_rigidbody.velocity, _rigidbody.transform.forward);
        var counterForwardVelocity = -forwardVelocity * factor;
        _rigidbody.AddForce(_rigidbody.transform.forward * counterForwardVelocity, ForceMode.VelocityChange);
    }

    public void SlowDownRotation()
    {
        _rotationSlowDownDiffTime += Time.deltaTime / _settings.SlowDownTotalTime;
        var factor = Mathf.Lerp(0, 1, _rotationSlowDownDiffTime);

        var v = _rigidbody.angularVelocity;
        var yCounterVelocity = -v.y * factor;
        _rigidbody.AddTorque(new Vector3(0, yCounterVelocity, 0), ForceMode.VelocityChange);
    }


    //***********************************


    public Vector3 Position => _rigidbody.transform.Find("Sk_Veh_Attack_Heli").position;
    public Quaternion Rotation => _rigidbody.transform.Find("Sk_Veh_Attack_Heli").rotation;
}