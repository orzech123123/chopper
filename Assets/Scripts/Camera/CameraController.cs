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
            Screen.SetResolution(1560, 720, true);

            _vcam = GetComponent<CinemachineFreeLook>();
            _vcam.m_YAxis.m_InputAxisName = "";
            _vcam.m_XAxis.m_InputAxisName = "";
        }

        void LateUpdate()
        {
            _vcam.m_RecenterToTargetHeading.m_enabled = true;

            _vcam.m_XAxis.Value = 0;
            _vcam.m_YAxis.Value = 0.5f;
            //Debug.Log(EventSystem.current.IsPointerOverGameObject());
            //if (UnityEngine.Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            //{
            //    _vcam.m_YAxis.m_InputAxisName = "Mouse Y";
            //    _vcam.m_XAxis.m_InputAxisName = "Mouse X";
            //}
            //else
            //{
            //    _vcam.m_YAxis.m_InputAxisName = "";
            //    _vcam.m_XAxis.m_InputAxisName = "";
            //}
        }

    }
}   