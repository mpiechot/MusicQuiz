#nullable enable

using MusicQuiz.Disk;
using MusicQuiz.Exceptions;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuizScreen : MonoBehaviour
{
    [SerializeField]
    private QuestionDisk? diskPrefab;

    [SerializeField]
    private RectTransform? questionParent;

    [SerializeField]
    private DiskPlayer? diskPlayer;

    [SerializeField]
    private TMP_InputField? answerInputField;

    private Assets.Scripts.QuizManagement.QuizManager? quizManager;

    private TMP_InputField AnswerInputField => answerInputField != null ? answerInputField : throw new SerializeFieldNotAssignedException();

    private void Start()
    {
        SerializeFieldNotAssignedException.ThrowIfNull(diskPlayer, nameof(diskPlayer));

        quizManager = new Assets.Scripts.QuizManagement.QuizManager(diskPlayer);
    }

    public void Show(CategoryDiskData categoryToload)
    {
        SerializeFieldNotAssignedException.ThrowIfNull(diskPrefab, nameof(diskPrefab));

        gameObject.SetActive(true);

        var shuffledQuestions = categoryToload.Questions.Shuffle().ToList();

        for (var i = 0; i < shuffledQuestions.Count; i++)
        {
            var question = shuffledQuestions[i];
            var disk = Instantiate(diskPrefab, questionParent);
            disk.Initialize((i + 1) + "", () => SelectQuestion(disk));
            disk.AssignQuestionData(question);
            Debug.Log("Created Question Disk for: " + question.DiskName);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void SelectQuestion(QuestionDisk questionDisk)
    {
        quizManager?.SelectDisk(questionDisk);

        AnswerInputField.text = questionDisk.Data.LastAnswer;
    }

    public void OnCheckAnswerPressed()
    {
        quizManager?.CheckAnswer(AnswerInputField.text);
    }
}
