using UnityEngine;

public class UserData {

    [SerializeField] public bool hasOnboarded;
    [SerializeField] public bool isNotificationsOn;
    [SerializeField] public bool isDataSet;

    public UserData() {
        hasOnboarded = false;
        isNotificationsOn = true;
    }
}
