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
            float logarithmBase = 10.0f;

            return Mathf.Pow(logarithmBase, decibels / referenceLevel);
        }
    }
}
