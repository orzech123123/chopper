using System;
using UnityEngine;
using Zenject;

public class ChopperRotorsController : ITickable
{
    readonly Transform _topRotor;
    readonly Transform _rearRotor;
    private float _enginesWarmUpDiffTime;
    private Settings _settings;

    public ChopperRotorsController(Transform topRotor, Transform rearRotor, Settings settings)
    {
        _topRotor = topRotor;
        _rearRotor = rearRotor;
        _settings = settings;
    }

    public void Tick()
    {
        _enginesWarmUpDiffTime += Time.deltaTime / _settings.EnginesWarmUpTotalTime;
        var factor = Mathf.Lerp(0, 1, _enginesWarmUpDiffTime);
        var speed = _settings.MaxSpeed * factor;

        _topRotor.localRotation = Quaternion.Euler(0, speed, 0) * _topRotor.localRotation;
        _rearRotor.localRotation = Quaternion.Euler(speed, 0, 0) * _rearRotor.localRotation;
    }

    [Serializable]
    public class Settings
    {
        public float MaxSpeed = 200f;
        public float EnginesWarmUpTotalTime = 60f;
    }
}