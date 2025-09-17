using Assets.AudioMixerPractice.Codebase.Sound.Enums;
using UnityEngine.Audio;

namespace Assets.AudioMixerPractice.Codebase.Sound
{
    public class VolumeChangerPresenter
    {
        private VolumeChanger _volumeChanger;
        private VolumePreferences _preferences;
        private VolumeCalculator _calculator;
        private AudioMixer _mixer;

        public VolumeChangerPresenter(VolumeChanger volumeChanger, VolumePreferences volumePreferences, VolumeCalculator calculator, AudioMixer mixer)
        {
            _volumeChanger = volumeChanger;
            _preferences = volumePreferences;
            _calculator = calculator;
            _mixer = mixer;

            _volumeChanger.Changed += UpdateModel;
            _volumeChanger.MuteSwitcherTriggered += OnMuteSwitcherTriggered;
            _preferences.Changed += UpdateView;

            LoadPreferences();
        }

        public void UpdateModel(Channels channel, float percentage)
        {
            float decibels = _calculator.ComputeDecibelsFromPercentage(percentage, AudioMixerStaticData.MaxVolume, AudioMixerStaticData.MinVolume);

            _preferences.SetVolumeLevel(channel, decibels);

            ChangeMixerLevel(channel, decibels);
        }

        private void UpdateView(Channels channel, float decibels) 
        {
            if (_preferences.IsMuted == false) 
            {
                float percentage = _calculator.ComputePercentageFromDecibels(decibels, AudioMixerStaticData.MaxVolume);

                _volumeChanger.SetChannelVolume(channel, percentage);

                ChangeMixerLevel(channel, decibels);
            }
        }

        private void OnMuteSwitcherTriggered(bool call) 
        {
            if (call)
            {
                _preferences.Mute();
                _mixer.SetFloat(AudioMixerStaticData.GetChannelName(Channels.Master), AudioMixerStaticData.MinVolume);
            }
            else 
            {
                _preferences.Unmute();
                LoadPreferences();
            }
        }

        private void ChangeMixerLevel(Channels channel, float decibels)
        {
            if (_preferences.IsMuted == false)
            {
                _mixer.SetFloat(AudioMixerStaticData.GetChannelName(channel), decibels);
            }
        }
        
        private void LoadPreferences() 
        {
            UpdateView(Channels.Master, _preferences.MasterDecibels);
            UpdateView(Channels.Effects, _preferences.EffectsDecibels);
            UpdateView(Channels.Music, _preferences.MusicDecibels);
        }
    }
}
