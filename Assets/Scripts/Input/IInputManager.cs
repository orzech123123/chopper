﻿namespace Assets.Scripts.Input
{
    public interface IInputManager
    {
        float LeftRightValue { get; }
        float ForwardValue { get; }
        float TurnOnXValue { get; }
        float TurnOnYValue { get; }
        bool IsLeftRightActive { get; }
        bool IsForwardActive { get; }
        bool IsTurnOnXActive { get; }
        bool IsTurnOnYActive { get; }
    }
}
