namespace Assets.Scripts.Input
{
    public interface IInputManager
    {
        float LeftRightValue { get; }
        float ForwardValue { get; }
        float VerticalValue { get; }
        float TurnValue { get; }
        bool IsLeftRightActive { get; }
        bool IsForwardActive { get; }
        bool IsVerticalActive { get; }
        bool IsTurnActive { get; }
    }
}
