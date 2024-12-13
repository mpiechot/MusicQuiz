#nullable enable

using Musicmania.Exceptions;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Musicmania
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource? audioSource;

        private AudioSource AudioSource => SerializeFieldNotAssignedException.ThrowIfNull(audioSource, nameof(audioSource));

        private AssetReferenceT<AudioClip>? currentClip;

        private AsyncOperationHandle<AudioClip>? currentHandle;

        public void LoadAndPlay(AssetReferenceT<AudioClip> audioToLoad)
        {
            // If the audio clip is already loaded and the handle is valid, play the audio clip.
            if (currentClip == audioToLoad && currentHandle.HasValue
                && currentHandle.Value.IsValid()
                && currentHandle.Value.Status == AsyncOperationStatus.Succeeded)
            {
                Restart();
                return;
            }

            if (currentClip != null && currentClip != audioToLoad)
            {
                currentClip?.ReleaseAsset();
            }

            currentClip = audioToLoad;

            currentHandle = audioToLoad.LoadAssetAsync();
            currentHandle.Value.Completed += OnAudioClipLoaded;
        }

        private void OnAudioClipLoaded(AsyncOperationHandle<AudioClip> handle)
        {
            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                throw new InvalidOperationException($"Failed to load AudioClip: {handle.OperationException?.Message}");
            }

            Play(handle.Result);
        }

        private void Play(AudioClip clip)
        {
            AudioSource.clip = clip;
            AudioSource.Play();
        }

        public void Stop()
        {
            AudioSource.Stop();
        }

        public void Restart()
        {
            if (currentClip == null || !currentClip.IsDone)
            {
                return;
            }

            AudioSource.Stop();
            AudioSource.Play();
        }
    }
}