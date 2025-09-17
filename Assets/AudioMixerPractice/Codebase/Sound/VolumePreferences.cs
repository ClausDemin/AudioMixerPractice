using Assets.AudioMixerPractice.Codebase.Sound.Enums;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Assets.AudioMixerPractice.Codebase.Sound
{
    public class VolumePreferences
    {
        private float _masterDecibels;
        private float _musicDecibels;
        private float _effectsDecibels;

        private Dictionary<Channels, FieldInfo> _channelVolume;
        private object _instance;

        public VolumePreferences(float master, float effects, float music)
        {
            _masterDecibels = master;
            _musicDecibels = music;
            _effectsDecibels = effects;

            InitChannelVolume();
            _instance = this;
        }

        public event Action<Channels, float> Changed;

        public float MasterDecibels => _masterDecibels;
        public float MusicDecibels => _musicDecibels;
        public float EffectsDecibels => _effectsDecibels;

        public bool IsMuted { get; private set; }

        public void Mute() 
        {
            IsMuted = true;
        }

        public void Unmute() 
        {
            IsMuted = false;
        }

        public void SetVolumeLevel(Channels channel, float decibels)
        {
            _channelVolume[channel].SetValue(_instance, decibels);

            Changed?.Invoke(channel, decibels);
        }
        
        private void InitChannelVolume()
        {
            Type type = typeof(VolumePreferences);

            _channelVolume = new Dictionary<Channels, FieldInfo>()
            {
                {Channels.Master, type.GetField(nameof(_masterDecibels), BindingFlags.Instance | BindingFlags.NonPublic) },
                {Channels.Music, type.GetField(nameof(_musicDecibels), BindingFlags.Instance | BindingFlags.NonPublic) },
                {Channels.Effects, type.GetField(nameof(_effectsDecibels), BindingFlags.Instance | BindingFlags.NonPublic) }

            };
        }
    }
}
