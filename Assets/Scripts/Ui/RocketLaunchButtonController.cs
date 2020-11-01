using Assets.Scripts.Chopper;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Ui
{
    public class RocketLaunchButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
		private RocketLauncher _chopperRocketLauncher;
        private bool _isHeld;

        public void OnPointerDown(PointerEventData eventData)
        {
            _isHeld = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isHeld = false;
        }

        void Update()
        {
            if (_isHeld)
            {
                _chopperRocketLauncher.TryLaunch();
            }
        }
    }
}
