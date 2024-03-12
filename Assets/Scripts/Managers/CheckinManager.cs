using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckinManager : MonoBehaviour
{

    public int curEmotionVal { get; set; }

    public List<int> emotionVals { get; set; } = new List<int> ();
    public List<string> reasonVals { get; set; } = new List<string> ();
    public bool hasCheckedIn { get; set; } = false;
    public bool isHistoryAvailable { get; set; } = false;

    //Alter to load in history from DB
}
