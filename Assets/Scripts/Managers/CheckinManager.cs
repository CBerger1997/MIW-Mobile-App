using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckinManager : MonoBehaviour, IDataPersistence
{
    public int emotionVal { get; set; }
    public int reasonVal { get; set; }
    public bool hasCheckedIn { get; set; } = false;

    public void LoadData ( UserData data )
    {
        //Not required for this view
    }

    public void SaveData ( ref UserData data )
    {
        if ( hasCheckedIn )
        {
            data.emotionValues.Add ( emotionVal );
            data.reasonValues.Add ( reasonVal );
            data.emotionDates.Add ( DateTime.Now.ToString ( "yyyy/MM/dd" ) );
        }
    }
}
