using Assets.AudioMixerPractice.Codebase.Sound.Enums;
using System;

namespace Assets.AudioMixerPractice.Codebase.Sound
{
    public class VolumePreferences
    {
        private float _decibels;

        public VolumePreferences(Channels channel, float initialLevel)
        {
            _decibels = initialLevel;
            Channel = channel;
        }

        public event Action<Channels, float> Changed;

        public Channels Channel { get; }
        public float Decibels => _decibels;

        public void SetVolumeLevel(float decibels)
        {
            _decibels = decibels;

            Changed?.Invoke(Channel, decibels);
        }
    }
}
