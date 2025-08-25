#nullable enable

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Musicmania.Questions
{
    public class QuestionSaveContainerManager
    {
        private readonly MusicmaniaContext musicQuizContext;

        private Dictionary<string, string> questionSaveMap = new();

        public QuestionSaveContainerManager(MusicmaniaContext musicQuizContext)
        {
            this.musicQuizContext = musicQuizContext;
        }

        private void EnsureSavesAreLoaded()
        {
            if (questionSaveMap.Count > 0)
            {
                return;
            }

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

        public QuestionSaveContainer GetQuestionSaveContainer(string questionName)
        {
            EnsureSavesAreLoaded();

            if (questionSaveMap.TryGetValue(questionName, out string questionSaveFilePath))
            {
                var questionSaveJson = File.ReadAllText(questionSaveFilePath);
                var questionSaveContainer = JsonConvert.DeserializeObject<QuestionSaveContainer>(questionSaveJson);

                return questionSaveContainer ?? throw new JsonSerializationException("Failed to deserialize question save container.");
            }

            var newQuestionSaveContainer = new QuestionSaveContainer();
            questionSaveMap.Add(questionName, Path.Combine(Application.persistentDataPath, musicQuizContext.Settings.ResourceSettings.SaveDataLocation, $"{questionName}.json"));
            return newQuestionSaveContainer;
        }

        public void SaveQuestionSaves(QuestionSaveContainer questionSaveContainer, string questionName)
        {
            var saveLocation = Path.Combine(Application.persistentDataPath, musicQuizContext.Settings.ResourceSettings.SaveDataLocation);

            if (!Directory.Exists(saveLocation))
            {
                // Create folder if it doesn't exist
                Debug.Log("Creating save location, because it doesn't exist yet.");
                Directory.CreateDirectory(Path.GetDirectoryName(saveLocation));
            }

            EnsureSavesAreLoaded();

            if (!questionSaveMap.TryGetValue(questionName, out string questionSaveContainerFilePath))
            {
                questionSaveContainerFilePath = Path.Combine(saveLocation, $"{questionName}.json");
                Debug.Log("Creating new save file for question, because it doesn't exist yet.");
                questionSaveMap.Add(questionName, questionSaveContainerFilePath);
            }

            Debug.Log($"Saving question save to: {questionSaveContainerFilePath}");
            var questionSaveContainerJson = JsonConvert.SerializeObject(questionSaveContainer, Formatting.Indented);
            File.WriteAllText(questionSaveContainerFilePath, questionSaveContainerJson);
        }
    }
}