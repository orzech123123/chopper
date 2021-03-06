﻿using UnityEngine;

namespace Assets.Scripts.Ui
{
    public class FpsDisplay : MonoBehaviour
    {
        private float _deltaTime;

        void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        }

        void OnGUI()
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 30;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
            float msec = _deltaTime * 1000.0f;
            float fps = 1.0f / _deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps ({2}) acc {3})", msec, fps, Screen.currentResolution, UnityEngine.Input.acceleration);
            GUI.Label(rect, text, style);
        }
    }
}