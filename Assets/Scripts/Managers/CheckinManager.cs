using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckinManager : MonoBehaviour, IDataPersistence {

    public int curEmotionVal { get; set; }

    public List<int> emotionVals { get; set; } = new List<int> ();
    public List<int> reasonVals { get; set; } = new List<int> ();
    public bool hasCheckedIn { get; set; } = false;

    public void LoadData ( UserData data ) {

    }

    public void SaveData ( ref UserData data ) {
        if ( hasCheckedIn ) {
            foreach ( int emotion in emotionVals ) {
                data.emotionValues.Add ( emotion );
            }
            foreach ( int reason in reasonVals ) {
                data.reasonValues.Add ( reason );
            }
            for ( int i = 0; i < emotionVals.Count; i++ ) {
                data.emotionDates.Add ( DateTime.Now.ToString ( "yyyy/MM/dd" ) );
            }

        }

        emotionVals.Clear ();
        reasonVals.Clear ();
    }
}
