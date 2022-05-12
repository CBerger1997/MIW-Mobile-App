using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuoteManager : MonoBehaviour {

    public List<string> author { get; set; }
    public List<string> quote { get; set; }
    public List<string> pathway { get; set; }

    public void ReadQuotesCSV() {
        string path = Application.dataPath + "/Documents/Excel/Quotes.csv";

        string fileData = System.IO.File.ReadAllText(path);

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
