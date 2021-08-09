//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;

//public class QuestionFactory : MonoBehaviour
//{
//    public Question CreateQuestion(GameObject question_prefab, Object file, int questionNumber, Transform parent)
//    {
//        AudioClip audio = (AudioClip)file;
//        string game = file.name.Substring(file.name.IndexOf("_") + 1);
//        string answer = game.Substring(0, game.IndexOf("_"));
//        string onlySerie = game.Substring(game.IndexOf("_") + 1);

//        //Regex rgx = new Regex(@"\d+$");
//        //answer = rgx.Replace(answer, "");

//        //Create Button
//        GameObject questionButton = Instantiate(question_prefab);
//        questionButton.transform.SetParent(parent, false);
//        questionButton.name = "Question " + questionNumber + ": " + answer;

//        TextMeshProUGUI questionText = questionButton.GetComponentInChildren<TextMeshProUGUI>();
//        questionText.text = "" + (questionNumber + 1);
//        question_buttons[i] = questionButton;
//        //questionButton.GetComponent<Button>().onClick.AddListener(delegate { playQuestion(questionText.text); });  

//        //Setup Question
//        Question question = questionButton.GetComponent<Question>();
//        question.source = gameObject.AddComponent<AudioSource>();
//        question.source.volume = 0f;
//        question.source.loop = false;
//        question.answer = answer;
//        question.source.clip = audio;
//        question.onlySeries = (onlySerie.Equals("true") ? true : false);

//        string oldAnswer = PlayerPrefs.GetString(answer);
//        if (oldAnswer != "")
//        {
//            question.last_checked = oldAnswer;
//            activeQuestion = question;
//            SetColorOnDistance(question, LevenshteinDistance(answer, question.last_checked));
//        }

//        return question;
//    }
//}
