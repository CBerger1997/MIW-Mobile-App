using System;
using System.Collections.Generic;

[Serializable]
public class UserData {
    //Initial Setup Variables
    public bool hasOnboarded;
    //public bool isDataSet;

    //Notification Variables
    public bool checkinNotificationsSet;
    public bool affirmationNotificationsSet;
    public bool journalNotificationsSet;
    public bool visualisationNotificationsSet;
    public bool breathingNotificationsSet;
    public bool commitmentsNotificationsSet;

    //Affirmation Variables
    public int AffSelection;
    public int AffListSelection;
    public string AffPersonalSelection;

    //User Private Variables
    public string username;
    public string password;

    //CheckIn Variables
    public List<string> emotionDates;
    public List<int> emotionValues;
    public List<string> reasonValues;
    public int date;
    public bool shouldSaveCheckin;

    //Journal Variables
    public List<string> dataEntries;

    public UserData () {
        hasOnboarded = false;

        checkinNotificationsSet = true;
        affirmationNotificationsSet = true;
        journalNotificationsSet = true;
        visualisationNotificationsSet = true;
        breathingNotificationsSet = true;
        commitmentsNotificationsSet = true;

        AffSelection = 0;
        AffListSelection = -1;
        AffPersonalSelection = "";

        username = "";
        password = "";

        emotionDates = new List<string> ();
        emotionValues = new List<int> ();
        reasonValues = new List<string> ();
        date = 0;
        shouldSaveCheckin = false;

        dataEntries = new List<string> ();
    }
}
