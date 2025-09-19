using Assets.AudioMixerPractice.Codebase.Sound;
using Assets.AudioMixerPractice.Codebase.Sound.Enums;
using UnityEngine.Audio;

namespace Assets.AudioMixerPractice.Codebase.UI.Toggles
{
    public class MuteTogglePresenter
    {
        private MuteService _muteService;
        private AudioMixer _mixer;
        private VolumePreferences _masterPreferences;
        private MuteToggle _view;

        public MuteTogglePresenter(MuteToggle view, AudioMixer mixer, MuteService muteService, VolumePreferences masterPreferences)
        {
            _muteService = muteService;
            _mixer = mixer;
            _masterPreferences = masterPreferences;
            _view = view;

            _view.Clicked += OnValueChanged;
        }

        private void OnValueChanged(bool isMuted) 
        {
            if (isMuted)
            {
                _muteService.Mute(_mixer, Channels.Master, AudioMixerStaticData.MinVolume);
            }
            else 
            {
                _muteService.Unmute(_mixer, Channels.Master, _masterPreferences.Decibels);
            }
        }
    }
}
