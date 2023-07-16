using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public static class AudioFades
    {
        private const float volumeThreshold = 0.01f;
        
        public static IEnumerator Fade(AudioSource audioSource, float duration, float targetVolume)
        {
            float startVolume = audioSource.volume;
            float currentTime = 0;

            while (Mathf.Abs(audioSource.volume - targetVolume) > volumeThreshold)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
                
                yield return null;
            }

            audioSource.volume = targetVolume;
        }

    }
}