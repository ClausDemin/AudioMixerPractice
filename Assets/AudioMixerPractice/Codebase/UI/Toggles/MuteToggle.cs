using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.AudioMixerPractice.Codebase.UI.Toggles
{
    public class MuteToggle: MonoBehaviour
    {
        private Toggle _toggle;

        public event Action<bool> Clicked;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }

        private void Start()
        {
            _toggle.onValueChanged.AddListener((bool call) => OnValueChanged(call));
        }

        private void OnValueChanged(bool call)
        {
            Clicked?.Invoke(call);
        }
    }
}
