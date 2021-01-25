using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        public string HorizontalInput = "Mouse X";
        public string VerticalInput = "Mouse Y";
        public CinemachineFreeLook _vcam;

        void Awake()
        {
            _vcam = GetComponent<CinemachineFreeLook>();
            _vcam.m_YAxis.m_InputAxisName = "";
            _vcam.m_XAxis.m_InputAxisName = "";
        }

        void LateUpdate()
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                _vcam.m_YAxis.m_InputAxisName = "Mouse Y";
                _vcam.m_XAxis.m_InputAxisName = "Mouse X";
            }
            else
            {
                _vcam.m_YAxis.m_InputAxisName = "";
                _vcam.m_XAxis.m_InputAxisName = "";
            }
        }

    }
}   