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
            AdjustCamera();
        }

        void LateUpdate()
        {
            if(UnityEngine.Input.GetMouseButton(0))
            {
                AdjustCamera();
            }
        }

        private void AdjustCamera()
        {
            var pointerX = UnityEngine.Input.GetAxis("Mouse X");
            //android:
            if(UnityEngine.Input.touchCount > 0)
            {
                pointerX = UnityEngine.Input.touches[0].deltaPosition.x / Screen.width * 100;
            }

            offset = Quaternion.AngleAxis(pointerX * turnSpeed, Vector3.up) * offset;
            transform.position = player.position + offset;
            transform.LookAt(player.position);
            transform.Rotate(dowwRotationAngle, 0f, 0);
        }
    }
}   