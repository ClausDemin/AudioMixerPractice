using Assets.AudioMixerPractice.Codebase.Sound.Enums;
using UnityEngine.Audio;

namespace Assets.AudioMixerPractice.Codebase.Sound
{
    public class VolumeChangerPresenter
    {
        private VolumeCalculator _calculator;

        private VolumeChanger _volumeChanger;
        private VolumePreferences _preferences;
        private AudioMixer _mixer;
        private MuteService _muteService;

        public VolumeChangerPresenter(VolumeCalculator calculator, MuteService muteService,
            VolumeChanger volumeChanger, VolumePreferences volumePreferences, AudioMixer mixer)
        {
            _calculator = calculator;
            _muteService = muteService;
            _volumeChanger = volumeChanger;
            _preferences = volumePreferences;
            _mixer = mixer;

            _volumeChanger.Changed += UpdateModel;
            _preferences.Changed += UpdateView;

            LoadPreferences();
        }

        public void UpdateModel(Channels channel, float percentage)
        {
            float decibels = _calculator.ComputeDecibelsFromPercentage(percentage, AudioMixerStaticData.MaxVolume, AudioMixerStaticData.MinVolume);

            _preferences.SetVolumeLevel(decibels);

            ChangeMixerLevel(channel, decibels);
        }

        private void UpdateView(Channels channel, float decibels)
        {
            float percentage = _calculator.ComputePercentageFromDecibels(decibels, AudioMixerStaticData.MaxVolume);

            _volumeChanger?.SetChannelVolume(channel, percentage);

            ChangeMixerLevel(channel, decibels);
        }

        private void ChangeMixerLevel(Channels channel, float decibels)
        {
            if (_muteService.IsMuted == false) 
            { 
                _mixer.SetFloat(AudioMixerStaticData.GetChannelName(channel), decibels);
            }
        }

        private void LoadPreferences()
        {
            UpdateView(Channels.Master, _preferences.Decibels);
        }
    }
}
