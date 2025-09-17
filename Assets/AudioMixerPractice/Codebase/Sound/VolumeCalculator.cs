using UnityEngine;

namespace Assets.AudioMixerPractice.Codebase.Sound
{
    public class VolumeCalculator
    {
        public float ComputeDecibelsFromPercentage(float percentage, float referenceLevel, float silenceLevel)
        {
            if (percentage == 0)
            {
                return silenceLevel;
            }

            return Mathf.Log10(percentage) * referenceLevel;
        }

        public float ComputePercentageFromDecibels(float decibels, float referenceLevel) 
        {
            return Mathf.Pow(10, decibels / referenceLevel);
        }
    }
}
