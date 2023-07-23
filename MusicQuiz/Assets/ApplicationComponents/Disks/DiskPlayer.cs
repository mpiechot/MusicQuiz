﻿#nullable enable

using MusicQuiz.Exceptions;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MusicQuiz.Disks
{
    public class DiskPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource? audioSource;

        private AudioSource AudioPlayer => audioSource != null ? audioSource : throw new SerializeFieldNotAssignedException();

        private AssetReferenceT<AudioClip>? currentClip;

        public void LoadAndPlay(AssetReferenceT<AudioClip> audioToLoad)
        {
            if (currentClip != audioToLoad)
            {
                currentClip?.ReleaseAsset();
            }

            currentClip = audioToLoad;

            audioToLoad.LoadAssetAsync().Completed += (handle) => Play(handle.Result);
        }

        private void Play(AudioClip clip)
        {
            AudioPlayer.clip = clip;
            AudioPlayer.Play();
        }

        public void Stop()
        {
            AudioPlayer.Stop();
        }

        public void Restart()
        {
            if (currentClip == null || !currentClip.IsDone)
            {
                return;
            }

            AudioPlayer.Stop();
            AudioPlayer.Play();
        }
    }
}