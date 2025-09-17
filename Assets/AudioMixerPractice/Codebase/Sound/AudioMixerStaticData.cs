using Assets.AudioMixerPractice.Codebase.Sound.Enums;
using System.Collections.Generic;
using UnityEngine.Audio;

namespace Assets.AudioMixerPractice.Codebase.Sound
{
    public static class AudioMixerStaticData
    {
        public const float MinVolume = -80;
        public const float MaxVolume = 20;

        private static readonly Dictionary<Channels, string> ChannelNames = new Dictionary<Channels, string>()
        {
            {Channels.Master, _masterVolume},
            {Channels.Music, _musicVolume},
            {Channels.Effects, _effectsVolume}
        };

        private static string _masterVolume => nameof(_masterVolume); 
        private static string _effectsVolume => nameof(_effectsVolume);
        private static string _musicVolume => nameof(_musicVolume);

        public static string GetChannelName(Channels channel) 
        {
            return ChannelNames[channel];
        }
    }
}
