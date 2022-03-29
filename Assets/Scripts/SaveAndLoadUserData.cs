using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveAndLoadUserData : MonoBehaviour {

    [System.Serializable]
    public class WrappingClass {
        public List<bool> _settings;
        public List<string> _loginData;

        public WrappingClass() {
            _settings = new List<bool>();
            _loginData = new List<string>();
        }
    }

    public static void SaveSettingsData(UserData uData) {

        WrappingClass settingsWrapper = new WrappingClass();

        settingsWrapper = SaveDataFromUserData(uData);

        string jsonStringToSave = JsonUtility.ToJson(settingsWrapper);
        string filePath = Path.Combine(Application.persistentDataPath, "Settings.data");

        File.WriteAllText(filePath, jsonStringToSave);
    }

    public static UserData LoadSettingsData() {
        string filePath = Path.Combine(Application.persistentDataPath, "Settings.data");

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

        dataWrapper._loginData.Add(uData.username);
        dataWrapper._loginData.Add(uData.password);

        return dataWrapper;
    }

    private static UserData LoadDataToUserData(WrappingClass dataWrapper) {
        UserData uData = new UserData();

        uData.hasOnboarded = dataWrapper._settings[0];
        uData.isNotificationsOn = dataWrapper._settings[1];
        uData.hasUserCheckedIn = dataWrapper._settings[2];

        uData.username = dataWrapper._loginData[0];
        uData.password = dataWrapper._loginData[1];

        return uData;
    }
}