using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveAndLoadUserData : MonoBehaviour {

    [System.Serializable]
    public class WrappingClass {
        public List<bool> _settings;
        public List<string> _loginData;

        public int selection;
        public int listSelection;
        public string personal;

        public List<string> _emotionDates;
        public List<int> _emotionValues;
        public List<int> _contextValues;

        public WrappingClass() {
            _settings = new List<bool>();
            _loginData = new List<string>();
            _emotionDates = new List<string>();
            _emotionValues = new List<int>();
            _contextValues = new List<int>();

            selection = 0;
            listSelection = -1;
            personal = "";
        }
    }

    public static void SaveUserData(UserData uData) {

        WrappingClass settingsWrapper = new WrappingClass();

        settingsWrapper = SaveDataFromUserData(uData);

        string jsonStringToSave = JsonUtility.ToJson(settingsWrapper);
        string filePath = Path.Combine(Application.persistentDataPath, "User.data");
        Debug.Log(Application.persistentDataPath);

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

        dataWrapper._emotionDates = (uData.emotionDates);
        dataWrapper._emotionValues = (uData.emotionValues);
        dataWrapper._contextValues = (uData.contextValues);

        return dataWrapper;
    }

    private static UserData LoadDataToUserData(WrappingClass dataWrapper) {
        UserData uData = new UserData();

        uData.hasOnboarded = dataWrapper._settings[0];
        uData.isNotificationsOn = dataWrapper._settings[1];

        if (dataWrapper._emotionDates.Count > 0 &&
            dataWrapper._emotionDates[dataWrapper._emotionDates.Count - 1] != System.DateTime.Now.ToString("yyyy/MM/dd") ||
            dataWrapper._emotionDates.Count < 1) {
            uData.hasUserCheckedIn = false;
        } else {
            uData.hasUserCheckedIn = true;
        }

        uData.userAffirmationSelection = dataWrapper.selection;
        uData.userAffirmationListSelection = dataWrapper.listSelection;
        uData.userAffirmationPersonalSelection = dataWrapper.personal;

        uData.username = dataWrapper._loginData[0];
        uData.password = dataWrapper._loginData[1];

        uData.emotionDates = dataWrapper._emotionDates;
        uData.emotionValues = dataWrapper._emotionValues;
        uData.contextValues = dataWrapper._contextValues;

        return uData;
    }
}