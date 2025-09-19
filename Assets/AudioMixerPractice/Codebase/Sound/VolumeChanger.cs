using Assets.AudioMixerPractice.Codebase.Sound.Enums;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.AudioMixerPractice.Codebase.Sound
{
    public class VolumeChanger : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public event Action<Channels, float> Changed;

        [field: SerializeField] public Channels Channel { get; private set; }

        private void Start()
        {
            SubscribeControlsEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeControlsEvents();
        }

        public void SetChannelVolume(Channels channel, float percentage)
        {
            _slider.SetValueWithoutNotify(percentage);
        }

        private void SubscribeControlsEvents()
        {
            _slider.onValueChanged.AddListener((float value) => OnChanged(_slider, value));
        }

        private void UnsubscribeControlsEvents()
        {
            _slider.onValueChanged.RemoveListener((float value) => OnChanged(_slider, value));
        }

        private void OnChanged(Slider control, float percentage)
        {
            SetChannelVolume(Channel, percentage);

            Changed?.Invoke(Channel, percentage);
        }
    }
}
