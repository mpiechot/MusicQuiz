using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class AssetToJsonConverter : MonoBehaviour
{
    [ContextMenu("Exportiere JSONs und Assets in Logdatei")]
    public void ExportJsonAndAssetListsToLog()
    {
        string assetFolder = @"C:\MARPIE\Projekte\Unity\MusicQuiz\MusicQuiz\Assets\Musicmania\_CreatedScriptableObjects\QuestionDisks\";
        string jsonFolder = @"C:\Users\Marco\AppData\LocalLow\DefaultCompany\MusicQuiz\saves\";

        string[] assetFiles = Directory.GetFiles(assetFolder, "*.asset")
    .OrderBy(f => Path.GetFileNameWithoutExtension(f).ToLowerInvariant())
    .ToArray();
        string[] jsonFiles = Directory.GetFiles(jsonFolder, "*.json")
    .OrderBy(f => Path.GetFileNameWithoutExtension(f).Replace(" ", "").ToLowerInvariant())
    .ToArray();


        List<string> lines = new List<string>();

        lines.Add($"==== JSON-Dateien ({jsonFiles.Length}) ====");
        for (int i = 0; i < jsonFiles.Length; i++)
        {
            lines.Add($"[JSON {i + 1}] {Path.GetFileName(jsonFiles[i])}");
        }

        lines.Add("");
        lines.Add($"==== ASSET-Dateien ({assetFiles.Length}) ====");
        for (int i = 0; i < assetFiles.Length; i++)
        {
            lines.Add($"[ASSET {i + 1}] {Path.GetFileName(assetFiles[i])}");
        }

        lines.Add("");
        if (jsonFiles.Length != assetFiles.Length)
        {
            lines.Add($"⚠️ Unterschiedliche Anzahl: {jsonFiles.Length} JSONs vs. {assetFiles.Length} Assets");
        }
        else
        {
            lines.Add("✅ Anzahl stimmt überein.");
        }

        string logPath = Path.Combine(Application.dataPath, "AssetJsonCheckLog.txt");
        File.WriteAllLines(logPath, lines);

        Debug.Log($"Dateiliste wurde gespeichert unter:\n{logPath}");
    }


    [ContextMenu("Update JSONs with Asset Values")]
    public void UpdateJsonWithAssetData()
    {
        string assetFolder = @"C:\MARPIE\Projekte\Unity\MusicQuiz\MusicQuiz\Assets\Musicmania\_CreatedScriptableObjects\QuestionDisks\";
        string jsonFolder = @"C:\Users\Marco\AppData\LocalLow\DefaultCompany\MusicQuiz\saves\";

        string[] assetFiles = Directory.GetFiles(assetFolder, "*.asset")
.OrderBy(f => Path.GetFileNameWithoutExtension(f).ToLowerInvariant())
.ToArray();
        string[] jsonFiles = Directory.GetFiles(jsonFolder, "*.json")
    .OrderBy(f => Path.GetFileNameWithoutExtension(f).Replace(" ", "").ToLowerInvariant())
    .ToArray();

        if (assetFiles.Length != jsonFiles.Length)
        {
            Debug.LogError($"Unterschiedliche Anzahl an Dateien: {assetFiles.Length} Assets vs. {jsonFiles.Length} JSONs");
            return;
        }

        for (int i = 0; i < assetFiles.Length; i++)
        {
            string assetContent = File.ReadAllText(assetFiles[i]);

            int tags = ExtractInt(assetContent, @"<Tags>k__BackingField:\s*(\d+)");
            bool exact = ExtractBool(assetContent, @"<AskForExactName>k__BackingField:\s*(\d+)");
            int difficulty = ExtractInt(assetContent, @"<Difficulty>k__BackingField:\s*(\d+)");

            string jsonPath = jsonFiles[i];
            string jsonContent = File.ReadAllText(jsonPath);

            // Versuche, existierendes JSON korrekt zu deserialisieren
            var wrapper = JsonUtility.FromJson<QuestionWrapper>(jsonContent);

            // Neue Werte setzen oder aktualisieren
            wrapper.exact = exact;
            wrapper.difficulty = difficulty;
            wrapper.tags = tags;
            wrapper.tips = new List<string>(); // immer leer laut deiner Beschreibung

            string updatedJson = JsonUtility.ToJson(wrapper, true);
            File.WriteAllText(jsonPath, updatedJson);
        }

        Debug.Log("Alle JSON-Dateien erfolgreich aktualisiert!");
    }

    private int ExtractInt(string content, string pattern)
    {
        var match = Regex.Match(content, pattern);
        return match.Success ? int.Parse(match.Groups[1].Value) : 0;
    }

    private bool ExtractBool(string content, string pattern)
    {
        var match = Regex.Match(content, pattern);
        return match.Success && match.Groups[1].Value == "1";
    }

    [Serializable]
    public class QuestionWrapper
    {
        public bool exact;
        public int difficulty;
        public int tags;
        public List<string> tips;
        public List<QuestionSave> questionSaves;
    }

    [Serializable]
    public class QuestionSave
    {
        public int categoryTags;
        public int position;
        public string lastAnswer;
        public int tippcount;
        public bool locked;
    }
}
