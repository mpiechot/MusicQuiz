using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [SerializeField]
    private bool DebugMode = false;
    [SerializeField]
    private GameObject questionPrefab;
    [SerializeField]
    private RectTransform parentTransform;
    [SerializeField]
    private TextMeshProUGUI categoryLbl;
    [SerializeField]
    private TextMeshProUGUI lastAnswerTxf;
    [SerializeField]
    private TextMeshProUGUI answerTxf;
    [SerializeField]
    private TextMeshProUGUI serieLbl;
    [SerializeField]
    private TextMeshProUGUI tippLbl;
    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private float questionWidth = 50f;

    private static QuizManager qm;
    private Questions questions;

    public QuestionComponent activeQuestion;

    public const int HELP_CHARACTER_NUMBER = 3;

    public static QuizManager GetQuizManager()
    {
        return qm;
    }


    void Awake()
    {
        qm = this;
    }

    void Start()
    {
        string category = PlayerPrefs.GetString("category", "games").ToLower();
        categoryLbl.text = category;

        //SetColorScheme();
        if(File.Exists(Path.Combine(Application.persistentDataPath, category + ".json")))
        {
            questions = JsonUtility.FromJson<Questions>(File.ReadAllText(
                Path.Combine(Application.persistentDataPath, category + ".json")));
        }
        else
        {
            TextAsset jsonText = (TextAsset)Resources.Load(category);
            questions = JsonUtility.FromJson<Questions>(jsonText.text);
        }
        

        Question[] sortedQuestions = questions.questions.OrderBy(q => q.id).ToArray();

        int maxNumber = 0;
        List<Question> newQuestions = InstantiateOldQuestions(category, ref maxNumber, sortedQuestions);

        reshuffle(newQuestions);

        InstantiateNewQuestions(category, maxNumber, newQuestions);
        Rect rect = parentTransform.rect;
        Debug.Log("1:"+parentTransform.rect.width);
        parentTransform.sizeDelta = new Vector2((newQuestions.Count() + sortedQuestions.Count()) * questionWidth, parentTransform.sizeDelta.y);
        Debug.Log("2:"+parentTransform.rect.width);
    }

    private List<Question> InstantiateOldQuestions(string category, ref int maxNumber, Question[] sortedQuestions)
    {
        List<Question> newQuestions = new List<Question>();
        foreach (Question question in sortedQuestions)
        {
            if (question.id == -1)
            {
                newQuestions.Add(question);
                continue;
            }
            if (question.id > maxNumber)
            {
                maxNumber = question.id;
            }
            InstantiateQuestion(question, category);
        }

        return newQuestions;
    }

    private void InstantiateNewQuestions(string category, int maxNumber, List<Question> unorderedQuestions)
    {
        foreach (Question question in unorderedQuestions)
        {
            question.id = maxNumber++;
            InstantiateQuestion(question, category);
        }
    }

    private void SetColorScheme()
    {
        string categoryColor = PlayerPrefs.GetString("catColor", "FFFFFFFF");
        ColorUtility.TryParseHtmlString(categoryColor, out Color color);
        categoryLbl.color = color;
        backgroundImage.color = color;
    }

    private void InstantiateQuestion(Question question,string category)
    {

        GameObject questionObject = Instantiate(questionPrefab);
        questionObject.transform.SetParent(parentTransform);

        AudioClip audio = Resources.Load<AudioClip>(category + "/" + question.sourcePath);
        CreateQuestion(audio, question, questionObject);
    }
    private void CreateQuestion(AudioClip audio, Question question, GameObject questionObject)
    {
        QuestionComponent questComp = questionObject.GetComponent<QuestionComponent>();
        questComp.question = question;
        questComp.name = "Question " + question.id;
        questComp.source.clip = audio;

        TextMeshProUGUI questionText = questComp.GetComponentInChildren<TextMeshProUGUI>();
        questionText.text = "" + (question.id + 1);

        if (PlayerPrefs.HasKey(question.sourcePath))
        {
            question.lastAnswer = PlayerPrefs.GetString(question.sourcePath);
            PlayerPrefs.DeleteKey(question.sourcePath);
        }      
        if(question.lastAnswer.Length > 1)
        {          
            CheckAnswer(questComp);
        }
    }
    
    public void SetActiveQuestion(QuestionComponent question)
    {
        activeQuestion = question;
        ShowTipps();
        if (activeQuestion.question.lastAnswer.Length > 1)
        {
            Debug.Log("Last Answer " + activeQuestion.question.id + ": " + activeQuestion.question.lastAnswer);
            lastAnswerTxf.text = activeQuestion.question.lastAnswer;
        }
        else
        {
            lastAnswerTxf.text = "";
        }
        if (activeQuestion.question.onlySeries)
        {
            serieLbl.text = "Gib den Name der Reihe an.";
        }
        else
        {
            serieLbl.text = "Gib den genauen Namen des Spiels an. Keine Abkürzungen.";
        }
    }
    public void StopAllSounds()
    {
        //foreach (GameObject btn in question_buttons)
        //{
        //    QuestionComponent question = btn.GetComponent<QuestionComponent>();
        //    if (activeQuestion == question)
        //        question.SoftStop();
        //    else
        //        question.HardStop();
        //}
    }
    public void CheckAnswer()
    {
        activeQuestion.question.lastAnswer = answerTxf.text;
        CheckAnswer(activeQuestion);
    }
    private void CheckAnswer(QuestionComponent questionComp)
    {
        string input = questionComp.question.lastAnswer;
        string answer = new string(input.ToLower().Where(c => !char.IsControl(c)).ToArray()).Substring(0, input.Length - 1);
        string correct = new string(questionComp.question.sourcePath.ToLower().Where(c => !char.IsControl(c)).ToArray());

        //Debug.Log("\nAnswer: \"" + answer.Length + "\"\nCorrect: \"" + correct.Length + "\"");
        int distance = LevenshteinDistance(answer, correct);
        //Debug.Log("Distance: " + distance);
        SetColorOnDistance(questionComp, distance);
    }

    private void SetColorOnDistance(QuestionComponent question, int distance)
    {
        if (distance == 0)
        {
            //Debug.Log("That's right!");
            question.GetComponentInChildren<Image>().color = Color.green;
        }
        else if (distance <= 3)
        {
            //Debug.Log("Close...");
            question.GetComponentInChildren<Image>().color = Color.yellow;
        }
        else
        {
            //Debug.Log("Nope!");
            question.GetComponentInChildren<Image>().color = Color.red;
        }
    }

    public void ExitGame()
    {
        if (DebugMode)
        {
            foreach (Question quest in questions.questions)
            {
                quest.id = -1;
                quest.lastAnswer = "?";
                quest.tipps[0] = "?";
                quest.actualTipLevel = 0;
            }
        }
        Application.Quit();
    }
    public void ToCategoryMenu()
    {
        if (DebugMode)
        {
            foreach (Question quest in questions.questions)
            {
                quest.id = -1;
                quest.lastAnswer = "?";
                quest.tipps[0] = "?";
                quest.actualTipLevel = 0;
            }
        }
        try
        {
            //Save Json
            string questionString = JsonUtility.ToJson(questions);
            string category = PlayerPrefs.GetString("category", "games");

            if (!Directory.Exists(Application.persistentDataPath))
            {
                Directory.CreateDirectory(Application.persistentDataPath); //Das geht wohl nicht :/ (und jetzt?)
            }
            File.WriteAllText(Path.Combine(Application.persistentDataPath, category + ".json"), questionString);

            //Load Scene
            SceneManager.LoadScene(0);
        }
        catch(Exception e)
        {
            tippLbl.text = e.Message;

        }
    }
    public void GetHelp()
    {
        if(activeQuestion != null && activeQuestion.question.actualTipLevel <= 2)
        {
            activeQuestion.question.actualTipLevel++;
            ShowTipps();
        }
    }
    private void ShowTipps()
    {
        int index = activeQuestion.question.actualTipLevel;
        if(index > 0)
        {
            if (activeQuestion.question.tipps[0].Equals("?"))
            {
                CreateTippOne();
            }
            tippLbl.text = "Tipp 1: " + activeQuestion.question.tipps[0];
            for (int i = 1; i < index; i++)
            {
                tippLbl.text += "\nTipp " + (i + 1) + ": " + activeQuestion.question.tipps[i];
            }
        }     
        else
        {
            tippLbl.text = "";
        }
    }
    private void CreateTippOne()
    {
        string answer = activeQuestion.question.sourcePath;
        string tipp = "";
        int[] answerCharacters = new int[HELP_CHARACTER_NUMBER];
        for (int i = 0; i < HELP_CHARACTER_NUMBER; i++)
        {
            int randomCharacter = UnityEngine.Random.Range(0, answer.Length);
            while(answer.ElementAt(randomCharacter) == ' ' || answerCharacters.Contains(randomCharacter))
            {
                randomCharacter = UnityEngine.Random.Range(0, answer.Length);
            }
            answerCharacters[i] = randomCharacter;
        }
        for (int i = 0; i < answer.Length; i++)
        {
            if (answer.ElementAt(i) == ' ')
            {
                tipp += "  ";
            }
            else
            {
                if (answerCharacters.Contains(i))
                {
                    tipp += answer.ElementAt(i) + " ";
                }
                else
                {
                    tipp += "_ ";
                }
            }
        }
        activeQuestion.question.tipps[0] = tipp;
    }
    public int LevenshteinDistance(string source, string target)
    {
        source = source.ToLower();
        target = target.ToLower();
        //Debug.Log("Source: \"" + source + "\"");
        //Debug.Log("target: \"" + target + "\"");
        if (string.IsNullOrEmpty(source))
        {
            if (string.IsNullOrEmpty(target)) return 0;
            return target.Length;
        }
        if (string.IsNullOrEmpty(target)) return source.Length;

        if (source.Length > target.Length)
        {
            var temp = target;
            target = source;
            source = temp;
        }

        var m = target.Length;
        var n = source.Length;
        var distance = new int[2, m + 1];
        // Initialize the distance matrix
        for (var j = 1; j <= m; j++) distance[0, j] = j;

        var currentRow = 0;
        for (var i = 1; i <= n; ++i)
        {
            currentRow = i & 1;
            distance[currentRow, 0] = i;
            var previousRow = currentRow ^ 1;
            for (var j = 1; j <= m; j++)
            {
                var cost = (target[j - 1] == source[i - 1] ? 0 : 1);
                distance[currentRow, j] = Math.Min(Math.Min(
                            distance[previousRow, j] + 1,
                            distance[currentRow, j - 1] + 1),
                            distance[previousRow, j - 1] + cost);
            }
        }
        return distance[currentRow, m];
    }
    void reshuffle(List<Question> questions)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < questions.Count; t++)
        {
            Question tmp = questions[t];
            int r = UnityEngine.Random.Range(t, questions.Count);
            questions[t] = questions[r];
            questions[r] = tmp;
        }
    }
}
