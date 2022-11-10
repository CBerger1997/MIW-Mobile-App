using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveAndLoadUserData : MonoBehaviour {

    public class AffirmationSettings {
        public int selection { get; set; }
        public int listSelection { get; set; }
        public string personal { get; set; }

        public AffirmationSettings() {
            selection = 0;
            listSelection = 0;
            personal = "";
        }
    }

    [System.Serializable]
    public class WrappingClass {
        public List<bool> _settings;
        public List<string> _loginData;
        public int selection;
        public int listSelection;
        public string personal;

        public WrappingClass() {
            _settings = new List<bool>();
            _loginData = new List<string>();
            selection = 0;
            listSelection = 0;
            personal = "";
        }
    }

    public static void SaveUserData(UserData uData) {

        WrappingClass settingsWrapper = new WrappingClass();

        settingsWrapper = SaveDataFromUserData(uData);

        string jsonStringToSave = JsonUtility.ToJson(settingsWrapper);
        string filePath = Path.Combine(Application.persistentDataPath, "User.data");

        File.WriteAllText(filePath, jsonStringToSave);
    }

    public static UserData LoadUserData() {
        string filePath = Path.Combine(Application.persistentDataPath, "User.data");

        if (File.Exists(filePath)) {

            string jsonStringToLoad = File.ReadAllText(filePath);

            List<bool> settingsBools = new List<bool>();
            WrappingClass dataWrapper = new WrappingClass();

            JsonUtility.FromJsonOverwrite(jsonStringToLoad, dataWrapper);

            UserData uData = new UserData();

            uData = LoadDataToUserData(dataWrapper);

            return uData;
        } else {
            return new UserData();
        }
    }

    //Use the classes below to alter what is saved and what is loaded
    //Ensure that the order is aligned with that of UserData class
    private static WrappingClass SaveDataFromUserData(UserData uData) {
        WrappingClass dataWrapper = new WrappingClass();

        dataWrapper._settings.Add(uData.hasOnboarded);
        dataWrapper._settings.Add(uData.isNotificationsOn);
        dataWrapper._settings.Add(uData.hasUserCheckedIn);

        dataWrapper.selection = (uData.userAffirmationSelection);
        dataWrapper.listSelection = (uData.userAffirmationListSelection);
        dataWrapper.personal = (uData.userAffirmationPersonalSelection);

        dataWrapper._loginData.Add(uData.username);
        dataWrapper._loginData.Add(uData.password);

        return dataWrapper;
    }

    private static UserData LoadDataToUserData(WrappingClass dataWrapper) {
        UserData uData = new UserData();

        uData.hasOnboarded = dataWrapper._settings[0];
        uData.isNotificationsOn = dataWrapper._settings[1];
        uData.hasUserCheckedIn = dataWrapper._settings[2];

        uData.userAffirmationSelection = dataWrapper.selection;
        uData.userAffirmationListSelection = dataWrapper.listSelection;
        uData.userAffirmationPersonalSelection = dataWrapper.personal;

        uData.username = dataWrapper._loginData[0];
        uData.password = dataWrapper._loginData[1];

        return uData;
    }
}