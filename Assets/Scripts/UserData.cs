using UnityEngine;

public class UserData {

    [SerializeField] public bool hasOnboarded;
    [SerializeField] public bool isNotificationsOn;

    public UserData() {
        hasOnboarded = false;
        isNotificationsOn = true;
    }
}
