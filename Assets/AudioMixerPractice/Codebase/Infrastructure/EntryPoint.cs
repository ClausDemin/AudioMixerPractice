using Assets.AudioMixerPractice.Codebase.Sound;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.AudioMixerPractice.Codebase.Infrastructure
{
    public class EntryPoint: MonoBehaviour
    {
        private const float initialVolumeLevel = -6;

        [SerializeField] private VolumeChanger _volumeChanger;
        [SerializeField] private AudioMixer _mixer;

        private VolumePreferences _volumePreferences = new VolumePreferences(initialVolumeLevel, initialVolumeLevel, initialVolumeLevel);
        private VolumeChangerPresenter _volumeChangerPresenter;

        private void Start()
        {
            _volumeChangerPresenter = new VolumeChangerPresenter(_volumeChanger, _volumePreferences, new VolumeCalculator(), _mixer);
        }
    }
}
