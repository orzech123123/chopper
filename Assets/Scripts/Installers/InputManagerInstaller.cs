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
            Container.Bind<IInputManager>().FromInstance(new MobileInputManager(_leftJoystick, _rightJoystick));
        }
    }
}