using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveAndLoadUserData : MonoBehaviour {

    [System.Serializable]
    public class WrappingClass {
        public List<bool> _settings;

        public WrappingClass() {
            _settings = new List<bool>();
        }
    }

    public static void SaveSettingsData(UserData uData) {

        WrappingClass settingsWrapper = new WrappingClass();

        settingsWrapper._settings.Add(uData.hasOnboarded);
        settingsWrapper._settings.Add(uData.isNotificationsOn);

        string jsonStringToSave = JsonUtility.ToJson(settingsWrapper);
        string filePath = Path.Combine(Application.persistentDataPath, "Settings.data");

        File.WriteAllText(filePath, jsonStringToSave);
    }

    public static UserData LoadSettingsData() {
        string filePath = Path.Combine(Application.persistentDataPath, "Settings.data");

        if (File.Exists(filePath)) {

            string jsonStringToLoad = File.ReadAllText(filePath);

            List<bool> settingsBools = new List<bool>();
            WrappingClass settingsWrapper = new WrappingClass();

            JsonUtility.FromJsonOverwrite(jsonStringToLoad, settingsWrapper);

            settingsBools = settingsWrapper._settings;

            UserData uData = new UserData();

            uData.hasOnboarded = settingsBools[0];
            uData.isNotificationsOn = settingsBools[1];

            return uData;
        } else {
            return new UserData();
        }
    }
}