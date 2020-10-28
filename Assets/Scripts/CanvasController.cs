using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CanvasController : MonoBehaviour
    {
		public Button yourButton;
		public Button yourButton2;

        void Start()
        {
            Button btn = yourButton.GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick);
            Button btn2 = yourButton2.GetComponent<Button>();
            btn2.onClick.AddListener(TaskOnClick2);
        }

        private void SwitchEnable(string name)
        {
            var go = Resources.FindObjectsOfTypeAll<GameObject>().First(goo => goo.name == name);
            go.SetActive(!go.activeInHierarchy);
        }

        void TaskOnClick()
        {
            SwitchEnable("Smoke");
        }

        void TaskOnClick2()
        {
            SwitchEnable("Buildings");
        }
	}
}
