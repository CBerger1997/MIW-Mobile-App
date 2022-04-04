using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVFileReader : MonoBehaviour {

    public static void ReadQuotesCSV() {
        string path = Application.dataPath + "/Documents/Excel/Quotes.csv";

        string fileData = System.IO.File.ReadAllText(path);

        string[] lines = fileData.Split("\n"[0]);

        List<string[]> lineData = new List<string[]>();

        foreach (string line in lines) {
            lineData.Add(line.Split(","[0]));
        }


        foreach (string[] lined in lineData) {
            foreach (string line in lined) {
                Debug.Log(line);
            }
        }
    }
}
