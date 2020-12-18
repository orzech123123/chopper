using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        public Vector3 offset;
        public float dowwRotationAngle = 15f;


        public float turnSpeed = 4.0f;
        public Transform player;

        private void Start()
        {
            AdjustCamera(true);
        }

        void LateUpdate()
        {
            if(UnityEngine.Input.GetMouseButton(0))
            {
                AdjustCamera();
            }
        }

        private void AdjustCamera(bool force = false)
        {
            var pointerX = UnityEngine.Input.GetAxis("Mouse X");
            var position = UnityEngine.Input.mousePosition.x;
            //android:
            if (UnityEngine.Input.touchCount > 0)
            {
                pointerX = UnityEngine.Input.touches[0].deltaPosition.x / Screen.width * 100;
                position = UnityEngine.Input.touches[0].rawPosition.x;
            }

            if((position < Screen.width * 0.3f || position > Screen.width * 2/3) && !force)
            {
                return;
            }

            offset = Quaternion.AngleAxis(pointerX * turnSpeed, Vector3.up) * offset;
            transform.position = player.position + offset;
            transform.LookAt(player.position);
            transform.Rotate(dowwRotationAngle, 0f, 0);
        }
    }
}   