using Assets.Scripts.Input;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class InputManagerInstaller : MonoInstaller
    {
        [SerializeField]
        private Joystick _leftJoystick;
        [SerializeField]
        private Joystick _rightJoystick;

        public override void InstallBindings()
        {
            #if UNITY_EDITOR
                Container.Bind<IInputManager>().FromInstance(new UiInputManager(_leftJoystick, _rightJoystick));
            #else
                 Container.Bind<IInputManager>().FromInstance(new AccelerometerInputManager(_leftJoystick, _rightJoystick));
            #endif
        }
    }
}