using Assets.AudioMixerPractice.Codebase.Sound.Enums;
using UnityEngine.Audio;

namespace Assets.AudioMixerPractice.Codebase.Sound
{
    public class MuteService
    {
        public bool IsMuted { get; private set; }

        public void Mute(AudioMixer mixer, Channels master, float silenceLevel) 
        {
            mixer.SetFloat(AudioMixerStaticData.GetChannelName(master), silenceLevel);
            IsMuted = true;
        }

        public void Unmute(AudioMixer mixer, Channels master, float level) 
        {
            mixer.SetFloat(AudioMixerStaticData.GetChannelName(master), level);
            IsMuted = false;
        }
    }
}
