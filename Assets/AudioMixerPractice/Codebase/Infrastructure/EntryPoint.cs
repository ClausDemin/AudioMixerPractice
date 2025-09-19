using Assets.AudioMixerPractice.Codebase.Sound;
using Assets.AudioMixerPractice.Codebase.Sound.Enums;
using Assets.AudioMixerPractice.Codebase.UI.Toggles;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.AudioMixerPractice.Codebase.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        private const float initialVolumeLevel = -6;

        private readonly List<VolumePreferences> _volumePreferences = new();
        private readonly List<VolumeChangerPresenter> _volumeChangerPresenters = new();
        
        private MuteTogglePresenter _muteTogglePresenter;

        [SerializeField] private VolumeChanger[] _volumeChangers;
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private MuteToggle _muteToggle;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            VolumeCalculator calculator = new VolumeCalculator();
            MuteService muteService = new MuteService();

            foreach (VolumeChanger volumeChanger in _volumeChangers)
            {
                VolumePreferences preferences = new VolumePreferences(volumeChanger.Channel, initialVolumeLevel);
                VolumeChangerPresenter presenter = new VolumeChangerPresenter(calculator, muteService, volumeChanger: volumeChanger, volumePreferences: preferences, _mixer);

                _volumePreferences.Add(preferences);
                _volumeChangerPresenters.Add(presenter);
            }

            _muteTogglePresenter = new MuteTogglePresenter(
                _muteToggle, _mixer, muteService,
                _volumePreferences
                .Where(preferences => preferences.Channel == Channels.Master)
                .First()
                );
        }

        private void OnDestroy()
        {
            _volumePreferences.Clear();
            _volumeChangerPresenters.Clear();
        }
    }
}
