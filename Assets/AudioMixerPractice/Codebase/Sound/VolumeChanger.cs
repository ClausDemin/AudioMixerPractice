using Assets.AudioMixerPractice.Codebase.Sound.Enums;
using Assets.AudioMixerPractice.Codebase.UI.Toggles;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.AudioMixerPractice.Codebase.Sound
{
    public class VolumeChanger : MonoBehaviour
    {
        [SerializeField] private MuteToggle _switcher;
        [SerializeField] private Slider _master;
        [SerializeField] private Slider _music;
        [SerializeField] private Slider _effects;

        private Dictionary<Slider, Channels> _channelControls;
        private Dictionary<Channels, Slider> _channels;

        public event Action<Channels, float> Changed;
        public event Action<bool> MuteSwitcherTriggered;

        private void Awake()
        {
            InitChannelControls();
            InitChannels();
        }

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
            _channels[channel].SetValueWithoutNotify(percentage);
        }

        private void SubscribeControlsEvents()
        {
            _master.onValueChanged.AddListener((float value) => OnChanged(_master, value));
            _music.onValueChanged.AddListener((float value) => OnChanged(_music, value));
            _effects.onValueChanged.AddListener((float value) => OnChanged(_effects, value));

            _switcher.Clicked += OnToggleChanged;
        }

        private void UnsubscribeControlsEvents()
        {
            _master.onValueChanged.RemoveListener((float value) => OnChanged(_master, value));
            _music.onValueChanged.RemoveListener((float value) => OnChanged(_music, value));
            _effects.onValueChanged.RemoveListener((float value) => OnChanged(_effects, value));

            _switcher.Clicked -= OnToggleChanged;
        }

        private void OnChanged(Slider control, float percentage)
        {
            Channels channel = _channelControls[control];

            SetChannelVolume(channel, percentage);

            Changed?.Invoke(channel, percentage);
        }

        private void OnToggleChanged(bool call)
        {
            MuteSwitcherTriggered?.Invoke(call);
        }

        private void InitChannelControls()
        {
            _channelControls = new Dictionary<Slider, Channels>()
            {
                {_master, Channels.Master},
                {_music, Channels.Music},
                {_effects, Channels.Effects},
            };
        }

        private void InitChannels()
        {
            _channels = new Dictionary<Channels, Slider>
            {
                {Channels.Master, _master },
                {Channels.Music, _music},
                {Channels.Effects, _effects},
            };
        }
    }
}
