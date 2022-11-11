using System.Collections.Generic;
using UnityEngine;

public class AffirmationManager : MonoBehaviour {
    public List<string> affirmationRandom { get; set; }
    public List<string> affirmationSelection { get; set; }

    public void ReadRandom() {
        string path = Application.dataPath + "/Documents/Excel/Affirmations Random.csv";

        affirmationRandom = ReadQuotesCSV(path);
    }

    public void ReadSelection() {
        string path = Application.dataPath + "/Documents/Excel/Affirmations Selection.csv";

        affirmationSelection = ReadQuotesCSV(path);
    }

    public List<string> ReadQuotesCSV(string path) {

        string fileData = System.IO.File.ReadAllText(path);

        fileData = fileData.Replace("\"", "");

        List<string> quoteFullLines = new List<string>(fileData.Split("\n"[0]));
        List<string> affirmation = new List<string>();

        foreach (string newLine in quoteFullLines) {

            affirmation.Add(newLine);
        }

        return affirmation;
    }
}
