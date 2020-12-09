using Assets.Scripts.Input;
using UnityEngine;
using Zenject;

public class InputManagerInstaller : MonoInstaller
{
    [SerializeField]
    private Joystick _leftJoystick;
    [SerializeField]
    private Joystick _rightJoystick;

    public override void InstallBindings()
    {
        Container.Bind<IInputManager>().FromInstance(new MobileInputManager(_leftJoystick, _rightJoystick));
    }
}