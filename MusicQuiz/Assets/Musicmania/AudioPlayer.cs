#nullable enable

using Musicmania.Exceptions;
using Musicmania.ResourceManagement;
using Musicmania.Utils;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Musicmania
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource? audioSource;

        private AudioSource AudioSource => SerializeFieldNotAssignedException.ThrowIfNull(audioSource, nameof(audioSource));

        private ResourceHandle<AudioClip>? currentHandle;
        private AudioClip? currentClip;
        private readonly CancellableTaskCollection taskCollection = new();

        /// <summary>
        ///     Loads the given audio clip and starts playback.
        /// </summary>
        /// <param name="audioToLoad">Handle to the audio resource to play.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="audioToLoad"/> is null.</exception>
        public void LoadAndPlay(ResourceHandle<AudioClip> audioToLoad)
        {
            if (audioToLoad == null)
            {
                throw new ArgumentNullException(nameof(audioToLoad));
            }

            if (currentHandle == audioToLoad && audioToLoad.IsLoaded)
            {
                Restart();
                return;
            }

            currentHandle = audioToLoad;

            taskCollection.CancelExecution();
            taskCollection.StartExecution(ct => LoadAndPlayAsync(audioToLoad, ct));
        }

        private async UniTask LoadAndPlayAsync(ResourceHandle<AudioClip> handle, CancellationToken cancellationToken)
        {
            var clip = await handle.LoadAsync(cancellationToken);
            currentClip = clip;
            Play(clip);
        }

        private void Play(AudioClip clip)
        {
            AudioSource.clip = clip;
            AudioSource.Play();
        }

        /// <summary>
        ///     Stops playback of the current audio clip.
        /// </summary>
        public void Stop()
        {
            AudioSource.Stop();
        }

        /// <summary>
        ///     Restarts playback of the loaded audio clip if available.
        /// </summary>
        public void Restart()
        {
            if (currentClip == null)
            {
                return;
            }

            AudioSource.Stop();
            AudioSource.Play();
        }

        private void OnDestroy()
        {
            taskCollection.Dispose();
        }
    }
}