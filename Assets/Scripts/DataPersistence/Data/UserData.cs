using System;
using System.Collections.Generic;

[Serializable]
public class UserData
{
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
    public bool shouldSaveCheckin;

    //Commitment Variables
    public string commitmentString;
    public int commitmentOftenVal;
    public bool isReminder;
    public int reminderOftenVal;
    public int reminderHourVal;
    public int reminderMinuteVal;
    public bool isCommitmentDone;
    public bool commitmentExists;

    public UserData ()
    {
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

        shouldSaveCheckin = false;

        commitmentString = "";
        commitmentOftenVal = 0;
        isReminder = false;
        reminderHourVal = 0;
        reminderMinuteVal = 0;
        isCommitmentDone = false;
        commitmentExists = false;
    }
}
