//using System.Text.RegularExpressions;
//using UnityEngine;
//using TMPro;
//using System;
//using System.Linq;
//using UnityEngine.UI;
//using System.Collections.Generic;

//public class GameManager : MonoBehaviour
//{
//    private static GameManager gm;
//    public TextMeshProUGUI input;
//    public TextMeshProUGUI placeholder;
//    public TextMeshProUGUI serie;
//    public TextMeshProUGUI tipp;
//    public TextMeshProUGUI debug;

//    public QuestionComponent activeQuestion;
//    private GameObject[] question_buttons;
//    public GameObject question_prefab;
//    public int score;
//    public Transform groubParent;

//    private Questions questions;

//    // Start is called before the first frame update
//    void Awake()
//    {
//        gm = this;

//        CreateQuestions();
//    }

//    private void CreateQuestions()
//    {
//        UnityEngine.Object[] files = Resources.LoadAll("games", typeof(AudioClip));
//        questions = SaveSystem.LoadQuestions();

//        OrderAndShuffleNewQuestions(questions,files);

//        question_buttons = new GameObject[files.Length];

//        //Set Container Width
//        RectTransform rowRectTransform = question_prefab.GetComponent<RectTransform>();
//        RectTransform containerRectTransform = groubParent.GetComponent<RectTransform>();
//        containerRectTransform.offsetMax = new Vector2(files.Length * rowRectTransform.rect.height, containerRectTransform.offsetMax.y);

//        for (int i = 0; i < files.Length; i++)
//        {
//            //Create SoundFile and Answer
//            UnityEngine.Object obj = files[i];
//            AudioClip audio = (AudioClip)obj;
//            string game = obj.name.Substring(obj.name.IndexOf("_") + 1);
//            string answer = game.Substring(0, game.IndexOf("_"));
//            string onlySerie = game.Substring(game.IndexOf("_") + 1);

//            //Create Button
//            GameObject questionButton = Instantiate<GameObject>(question_prefab);
//            questionButton.transform.SetParent(groubParent, false);
//            questionButton.name = "Question " + i + ": " + answer;

//            TextMeshProUGUI questionText = questionButton.GetComponentInChildren<TextMeshProUGUI>();
//            questionText.text = "" + (i + 1);
//            question_buttons[i] = questionButton; 

//            //Setup Question
//            CreateQuestion(audio, answer, onlySerie, questionButton);
//        }
//    }

//    private void OrderAndShuffleNewQuestions(Questions questions, UnityEngine.Object[] files)
//    {
//        if (questions == null || files.Length != questions.order.Count)
//        {
//            reshuffle(files);
//            List<string> order = new List<string>();
//            foreach (UnityEngine.Object file in files)
//            {
//                order.Add(file.name);
//            }
//            questions = new Questions(order);
//            SaveSystem.SaveQuestions(questions);
//        }
//        else
//        {
//            List<string> order = questions.order;
//            for (int orderedIndex = 0; orderedIndex < order.Count; orderedIndex++)
//            {
//                for (int outdatedIndex = 0; outdatedIndex < files.Length; outdatedIndex++)
//                {
//                    if (files[outdatedIndex].name.Equals(order[orderedIndex]))
//                    {
//                        UnityEngine.Object temp = files[orderedIndex];
//                        files[orderedIndex] = files[outdatedIndex];
//                        files[outdatedIndex] = temp;
//                    }
//                }
//            }
//        }
//    }

//    private void CreateQuestion(AudioClip audio, string answer, string onlySerie, GameObject questionButton)
//    {
//        QuestionComponent question = questionButton.GetComponent<QuestionComponent>();
//        question.source = gameObject.AddComponent<AudioSource>();
//        question.source.volume = 0f;
//        question.source.loop = false;
//        question.answer = answer;
//        question.source.clip = audio;
//        question.onlySeries = (onlySerie.Equals("true") ? true : false);

//        UpdateColorWhenLastAnswerExists(answer, question);
//    }

//    private void UpdateColorWhenLastAnswerExists(string answer, QuestionComponent question)
//    {
//        string oldAnswer = PlayerPrefs.GetString(answer);
//        if (oldAnswer != "")
//        {
//            question.last_checked = oldAnswer;
//            activeQuestion = question;
//            SetColorOnDistance(question, LevenshteinDistance(answer, question.last_checked));
//        }
//    }

//    public void SetActiveQuestion(QuestionComponent question)
//    {
//        activeQuestion = question;
//        //debug.text = question.answer;
//        if(!activeQuestion.last_checked.Equals(""))
//        {
//            placeholder.text = activeQuestion.last_checked;
//        }
//        if (activeQuestion.onlySeries)
//        {
//            serie.text = "Gib den Name der Reihe an.";
//        }
//        else
//        {
//            serie.text = "Gib den genauen Namen des Spiels an. Keine Abkürzungen.";
//        }
//    }
//    public void StopAllSounds()
//    {
//        foreach(GameObject btn in question_buttons)
//        {
//            QuestionComponent question = btn.GetComponent<QuestionComponent>();
//            if (activeQuestion == question)
//                question.SoftStop();
//            else
//                question.HardStop();
//        }
//    }

//    public static GameManager getGameManager()
//    {
//        return gm;
//    }
//    public void CheckAnswer()
//    {
//        activeQuestion.last_checked = input.text;

//        string answer = new string(input.text.ToLower().Where(c => !char.IsControl(c)).ToArray()).Substring(0, input.text.Length - 1);
//        string correct = new string(activeQuestion.answer.ToLower().Where(c => !char.IsControl(c)).ToArray());
//        PlayerPrefs.SetString(activeQuestion.answer, answer);

//        //Debug.Log("\nAnswer: \"" + answer.Length + "\"\nCorrect: \"" + correct.Length + "\"");
//        int distance = LevenshteinDistance(answer, correct);
//        //Debug.Log("Distance: " + distance);
//        SetColorOnDistance(activeQuestion,distance);
//    }

//    private void SetColorOnDistance(QuestionComponent question, int distance)
//    {
//        //Debug.Log("Question: " + question.answer + " => " + distance);
//        if (distance == 0)
//        {
//            //Debug.Log("That's right!");
//            question.GetComponent<Image>().color = Color.green;
//        }
//        else if (distance <= 3)
//        {
//            //Debug.Log("Close...");
//            question.GetComponent<Image>().color = Color.yellow;
//        }
//        else
//        {
//            //Debug.Log("Nope!");
//            question.GetComponent<Image>().color = Color.red;
//        }
//    }

//    public void ExitGame()
//    {
//        Application.Quit();
//    }
//    public void GetHelp()
//    {
//        tipp.text = "Der Titel des Spiels beginnt mit " + activeQuestion.answer.Substring(0, 3) + "...";
//    }
//    public int LevenshteinDistance(string source, string target)
//    {
//        source = source.ToLower();
//        target = target.ToLower();
//        //Debug.Log("Source: \"" + source + "\"");
//        //Debug.Log("target: \"" + target + "\"");
//        if (string.IsNullOrEmpty(source))
//        {
//            if (string.IsNullOrEmpty(target)) return 0;
//            return target.Length;
//        }
//        if (string.IsNullOrEmpty(target)) return source.Length;

//        if (source.Length > target.Length)
//        {
//            var temp = target;
//            target = source;
//            source = temp;
//        }

//        var m = target.Length;
//        var n = source.Length;
//        var distance = new int[2, m + 1];
//        // Initialize the distance matrix
//        for (var j = 1; j <= m; j++) distance[0, j] = j;

//        var currentRow = 0;
//        for (var i = 1; i <= n; ++i)
//        {
//            currentRow = i & 1;
//            distance[currentRow, 0] = i;
//            var previousRow = currentRow ^ 1;
//            for (var j = 1; j <= m; j++)
//            {
//                var cost = (target[j - 1] == source[i - 1] ? 0 : 1);
//                distance[currentRow, j] = Math.Min(Math.Min(
//                            distance[previousRow, j] + 1,
//                            distance[currentRow, j - 1] + 1),
//                            distance[previousRow, j - 1] + cost);
//            }
//        }
//        return distance[currentRow, m];
//    }
//    void reshuffle(UnityEngine.Object[] texts)
//    {
//        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
//        for (int t = 0; t < texts.Length; t++)
//        {
//            UnityEngine.Object tmp = texts[t];
//            int r = UnityEngine.Random.Range(t, texts.Length);
//            texts[t] = texts[r];
//            texts[r] = tmp;
//        }
//    }
//}
