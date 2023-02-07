using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class QuoteManager : MonoBehaviour {
    public List<string> author { get; set; }
    public List<string> quote { get; set; }
    public List<string> pathway { get; set; }

    public void ReadQuotesCSV() {
        string path;
        string fileData;

#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
        path = Application.dataPath + "/Documents/Excel/Quotes.csv";
        fileData = File.ReadAllText(path);
#endif

#if UNITY_ANDROID
        path = Path.Combine(Application.streamingAssetsPath, "Quotes.csv");
        var loadingRequest = UnityWebRequest.Get(path);
        loadingRequest.SendWebRequest();
        while (!loadingRequest.isDone && !loadingRequest.isNetworkError && !loadingRequest.isHttpError) ;
        fileData = System.Text.Encoding.UTF8.GetString(loadingRequest.downloadHandler.data);
#endif

        fileData = fileData.Replace("\"", "");

        List<string> quoteFullLines = new List<string>(fileData.Split("\n"[0]));

        quoteFullLines.RemoveAt(0);
        quoteFullLines.RemoveAt(quoteFullLines.Count - 1);

        author = new List<string>();
        quote = new List<string>();
        pathway = new List<string>();

        foreach (string newLine in quoteFullLines) {
            List<string> line = new List<string>(newLine.Split("*"[0]));

            line[1] = line[1].Remove(0, 1);
            line[2] = line[2].Remove(0, 1);

            author.Add(line[0]);
            quote.Add(line[1]);
            pathway.Add(line[2]);
        }
    }
}
