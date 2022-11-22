using System.Collections.Generic;
using UnityEngine;

public class UserData {

    [SerializeField] public bool hasOnboarded;
    [SerializeField] public bool isNotificationsOn;
    [SerializeField] public bool isDataSet;
    [SerializeField] public bool hasUserCheckedIn;

    [SerializeField] public int userAffirmationSelection;
    [SerializeField] public int userAffirmationListSelection;

    [SerializeField] public string userAffirmationPersonalSelection;

    [SerializeField] public string username;
    [SerializeField] public string password;

    [SerializeField] public List<string> emotionDates;
    [SerializeField] public List<int> emotionValues;
    [SerializeField] public List<int> contextValues;

    [SerializeField] public int date;


    public UserData() {
        hasOnboarded = false;
        isNotificationsOn = true;
        hasUserCheckedIn = false;

        userAffirmationSelection = 0;
        userAffirmationListSelection = -1;
        userAffirmationPersonalSelection = "";

        username = "";
        password = "";

        emotionDates = new List<string>();
        emotionValues = new List<int>();
        contextValues = new List<int>();
    }
}
