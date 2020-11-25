using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Chopper
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
            if(Input.GetMouseButton(0))
            {
                AdjustCamera();
            }
        }

        private void AdjustCamera()
        {
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
            transform.position = player.position + offset;
            transform.LookAt(player.position);
            transform.Rotate(dowwRotationAngle, 0f, 0);
        }
    }
}   