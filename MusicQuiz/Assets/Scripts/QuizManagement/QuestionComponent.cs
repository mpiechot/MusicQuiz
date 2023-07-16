using UnityEngine;
using TMPro;
using Assets.Scripts;
using UnityEngine.UI;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using Assets.Scripts.Questions;

public class QuestionComponent : MonoBehaviour
{
    [SerializeField] private AudioSource source = default;
    [SerializeField] private Question question = default;
    [SerializeField] private TMP_Text questionText = default;
    [SerializeField] private Image questionImage = default;
    [SerializeField] private ColorProfile colorProfile = default;
    [SerializeField, Range(0,1)] private float playVolume = 0.8f;

    private const float fadePlayDuration = 2f;
    private const float fadeEndDuration = 3f;

    private Action<QuestionComponent> onPlayQuestion;
    //private ToggleGraphics toggleGraphics;

    private CancellationTokenSource cts;

    private float startSoftStopAfter => source.clip.length - fadeEndDuration;

    public int QuestionId => question.id;

    public Question Question => question;
    
    public void ChangeQuestionState(QuestionState state)
    {
        switch (state)
        {
            case QuestionState.SOLVED:
                questionImage.color = colorProfile.solvedColor;
                //toggleGraphics.BackgroundImage.color = colorProfile.solvedColor;
                break;
            case QuestionState.UNKNOWN:
                questionImage.color = colorProfile.unknownColor;
                //toggleGraphics.BackgroundImage.color = colorProfile.unknownColor;
                break;
            case QuestionState.WRONG:
                questionImage.color = colorProfile.wrongColor;
                //toggleGraphics.BackgroundImage.color = colorProfile.wrongColor;
                break;
            case QuestionState.CLOSE_TO_SOLVED:
                questionImage.color = colorProfile.closeToSolvedColor;
                //toggleGraphics.BackgroundImage.color = colorProfile.closeToSolvedColor;
                break;
            default:
                questionImage.color = colorProfile.unknownColor;
                //toggleGraphics.BackgroundImage.color = colorProfile.unknownColor;
                break;
        }
    }

    public void Initialize(Question question, Action<QuestionComponent> onPlayQuestion)
    {
        AudioClip audio = Resources.Load<AudioClip>(question.sourcePath);
        this.onPlayQuestion = onPlayQuestion;
        this.question = question;
        name = "Question " + question.id;
        source.clip = audio;
        source.volume = 0;
        questionText.text = $"#{question.id}";
        //this.toggleGraphics = toggleGraphics;
    }

    public void Play()
    {
        if (source == null)
        {
            Debug.LogError($"[QuestionComponent] The source of {gameObject.name} was null when trying to play!");
            return;
        }
        onPlayQuestion.Invoke(this);
        Debug.Log("Play! " + question.sourcePath);
        cts = new CancellationTokenSource();
        PlayQuestion(cts.Token);
    }

    public void Stop()
    {
        if (source == null)
        {
            Debug.LogError($"[QuestionComponent] The source of {gameObject.name} was null when trying to stop!");
            return;
        }
        cts?.Cancel();
        source.Stop();
    }

    private async UniTask PlayQuestion(CancellationToken token)
    {
        Debug.Log("Playing!");
        source.Play();
        while(source.time < startSoftStopAfter)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            if(source.time < fadePlayDuration)
            {
                source.volume = Mathf.Lerp(0, playVolume, source.time / fadePlayDuration);
            }
            
            await UniTask.NextFrame(token);
        }
        
        while(source.time < source.clip.length)
        {
            if (token.IsCancellationRequested)
            {
                return;
            }
            source.volume = Mathf.Lerp(playVolume, 0, (source.time - startSoftStopAfter) / fadeEndDuration);
            await UniTask.NextFrame(token);
        }
        Debug.Log("Finished");
    }

    private void OnApplicationQuit()
    {
        cts?.Cancel();
    }
}
