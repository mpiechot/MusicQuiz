#nullable enable

using Musicmania.Data;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Musicmania
{
    public class SaveManager
    {
        private readonly MusicmaniaContext musicQuizContext;

        private Dictionary<string, string> questionSaveMap = new();

        public SaveManager(MusicmaniaContext musicQuizContext)
        {
            this.musicQuizContext = musicQuizContext;
        }

        public void LoadQuestionSaves()
        {
            questionSaveMap.Clear();
            var saveLocation = Path.Combine(Application.persistentDataPath, musicQuizContext.Settings.ResourceSettings.SaveDataLocation);

            if (!Directory.Exists(saveLocation))
            {
                // Create folder if it doesn't exist
                Debug.Log("Creating save location, because it doesn't exist yet.");
                Directory.CreateDirectory(Path.GetDirectoryName(saveLocation));
                return;
            }

            // Get all file paths in the save location
            foreach (var path in Directory.GetFiles(saveLocation))
            {
                Debug.Log($"Found save file: {path}");
                questionSaveMap.Add(Path.GetFileNameWithoutExtension(path), path);
            }
        }

        public QuestionSave? TryGetQuestionSave(string questionName)
        {
            if (questionSaveMap.TryGetValue(questionName, out string questionSaveFilePath))
            {
                var questionSaveJson = File.ReadAllText(questionSaveFilePath);
                return JsonUtility.FromJson<QuestionSave>(questionSaveJson);
            }

            return null;
        }

        public void SaveQuestionSaves(QuestionData question)
        {
            var saveLocation = Path.Combine(Application.persistentDataPath, musicQuizContext.Settings.ResourceSettings.SaveDataLocation);

            if (!Directory.Exists(saveLocation))
            {
                // Create folder if it doesn't exist
                Debug.Log("Creating save location, because it doesn't exist yet.");
                Directory.CreateDirectory(Path.GetDirectoryName(saveLocation));
            }

            if (!questionSaveMap.TryGetValue(question.name, out string questionSaveFilePath))
            {
                Debug.Log("Creating new save file for question, because it doesn't exist yet.");
                questionSaveFilePath = Path.Combine(saveLocation, $"{question.name}.json");
                questionSaveMap.Add(question.name, questionSaveFilePath);
            }

            Debug.Log($"Saving question save to: {questionSaveFilePath}");
            var questionSaveJson = JsonUtility.ToJson(question.QuestionSave);
            File.WriteAllText(Path.Combine(questionSaveFilePath, question.name), questionSaveJson);
        }
    }
}