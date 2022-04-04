using UnityEngine;

public class UserData {

    [SerializeField] public bool hasOnboarded;
    [SerializeField] public bool isNotificationsOn;
    [SerializeField] public bool isDataSet;
    [SerializeField] public bool hasUserCheckedIn;

    [SerializeField] public string username;
    [SerializeField] public string password;

    public UserData() {
        hasOnboarded = false;
        isNotificationsOn = true;
        hasUserCheckedIn = false;

        username = "";
        password = "";
    }
}
