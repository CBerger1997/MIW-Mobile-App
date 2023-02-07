using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class AffirmationManager : MonoBehaviour {
    public List<string> affirmationRandom { get; set; }
    public List<string> affirmationSelection { get; set; }

    public void ReadRandom() {
        string path;

#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
        path = Application.dataPath + "/Documents/Excel/Affirmations Random.csv";
#endif

#if UNITY_ANDROID
        path = Path.Combine(Application.streamingAssetsPath, "Affirmations Random.csv");
#endif

        affirmationRandom = ReadQuotesCSV(path);
    }

    public void ReadSelection() {
        string path;

#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
        path = path = Application.dataPath + "/Documents/Excel/Affirmations Selection.csv";
#endif

#if UNITY_ANDROID
        path = Path.Combine(Application.streamingAssetsPath, "Affirmations Selection.csv");
#endif

        affirmationSelection = ReadQuotesCSV(path);
    }

    public List<string> ReadQuotesCSV(string path) {
        string fileData;

#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
        fileData = File.ReadAllText(path);
#endif

#if UNITY_ANDROID
        var loadingRequest = UnityWebRequest.Get(path);
        loadingRequest.SendWebRequest();
        while (!loadingRequest.isDone && !loadingRequest.isNetworkError && !loadingRequest.isHttpError) ;
        fileData = System.Text.Encoding.UTF8.GetString(loadingRequest.downloadHandler.data);
#endif

        fileData = fileData.Replace("\"", "");

        List<string> quoteFullLines = new List<string>(fileData.Split("\n"[0]));
        List<string> affirmation = new List<string>();

        foreach (string newLine in quoteFullLines) {

            affirmation.Add(newLine);
        }

        return affirmation;
    }
}
