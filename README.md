# MusicQuiz
This is a Quiz-App with different categories. For example in the game category you have to listen to 15s of a game theme and answer with the correct game title.
Because there are many questions and thus many mp3 files, I set up unity to load the mp3-files in background so that the app is starting up faster. As mentioned in my PianoTutor App, the questions here are stored in json files (one file per category). Those questions are easily extendable and using them with a Json Deserializer is very easy.

### Keypoints
- Load mp3-Files in background
- Read Json-Files in C#
- Audiomanager
- UI Layout

### Some Code examples
**Creation of Questions and checking if they were already answered by reading the PlayerPrefs**
```c#
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
```
