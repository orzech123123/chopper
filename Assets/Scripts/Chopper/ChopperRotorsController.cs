using System;
using UnityEngine;
using Zenject;


[Serializable]
public class ChopperRotorsControllerSettings
{
    public float MaxSpeed = 200f;
    public float EnginesWarmUpTotalTime = 60f;
}

public class ChopperRotorsController : ITickable
{
    private float _enginesWarmUpDiffTime;
    private ChopperRotorsControllerSettings _settings;
    private ChopperPlayer _player;

    public ChopperRotorsController(ChopperPlayer player, ChopperRotorsControllerSettings settings)
    {
        _player = player;
        _settings = settings;
    }

    public void Tick()
    {
        _enginesWarmUpDiffTime += Time.deltaTime / _settings.EnginesWarmUpTotalTime;
        var factor = Mathf.Lerp(0, 1, _enginesWarmUpDiffTime);
        var speed = _settings.MaxSpeed * factor;

        _player.SetTopRotorSpeed(speed);
        _player.SetRearRotorSpeed(speed);
    }
}