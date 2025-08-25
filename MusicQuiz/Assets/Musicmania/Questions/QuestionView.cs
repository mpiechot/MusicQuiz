#nullable enable

using Cysharp.Threading.Tasks;
using Musicmania.Exceptions;
using Musicmania.ResourceManagement;
using Musicmania.Utils;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Musicmania.Questions
{
    public class QuestionView : MonoBehaviour
    {
        [SerializeField]
        private Image? image;

        [SerializeField]
        private AudioSource? audioSource;

        private QuestionData? questionData;
        private bool isInitialized;
        private ResourceHandle<Sprite>? imageResourceHandle;
        private ResourceHandle<AudioClip>? audioResourceHandle;
        private CancellableTaskCollection taskCollection = new();

        private Image Image => SerializeFieldNotAssignedException.ThrowIfNull(image);

        private AudioSource AudioSource => SerializeFieldNotAssignedException.ThrowIfNull(audioSource);

        private ResourceHandle<Sprite> ImageResourceHandle => NotInitializedException.ThrowIfNull(imageResourceHandle);

        private ResourceHandle<AudioClip> AudioResourceHandle => NotInitializedException.ThrowIfNull(audioResourceHandle);

        public void Initialize(QuestionData questionData, MusicmaniaContext context)
        {
            if (isInitialized)
            {
                return;
            }

            this.questionData = questionData;
            imageResourceHandle = context.ResourceManager.GetResource<Sprite>(questionData.ImageResourceKey);
            audioResourceHandle = context.ResourceManager.GetResource<AudioClip>(questionData.AudioResourceKey);
            taskCollection.StartExecution(InitializeAsync);

            isInitialized = true;
        }

        private async UniTask InitializeAsync(CancellationToken cancellationToken)
        {
            var sprite = await ImageResourceHandle.LoadAsync(cancellationToken);
            Image.sprite = sprite;

            var audio = await AudioResourceHandle.LoadAsync(cancellationToken);
            AudioSource.clip = audio;
        }

        private void OnDestroy()
        {
            taskCollection.Dispose();

            Image.sprite = null;
            AudioSource.clip = null;

            ImageResourceHandle.Unload();
            AudioResourceHandle.Unload();
        }
    }
}
