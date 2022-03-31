using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveAndLoadTableData : MonoBehaviour {

    [System.Serializable]
    public class WrappingClass {
        public List<string> _userLoginData;

        public WrappingClass() {
            _userLoginData = new List<string>();
        }
    }

    public static void SaveTableData(TableData tData) {
        WrappingClass tableWrapper = new WrappingClass();

        tableWrapper = SaveDataFromTableData(tData);

        string jsonStringToSave = JsonUtility.ToJson(tableWrapper);
        string filePath = Path.Combine(Application.persistentDataPath, "Table.data");

        File.WriteAllText(filePath, jsonStringToSave);
    }

    public static TableData LoadTableData() {
        string filePath = Path.Combine(Application.persistentDataPath, "Table.data");

        if (File.Exists(filePath)) {

            string jsonStringToLoad = File.ReadAllText(filePath);

            List<bool> settingsBools = new List<bool>();
            WrappingClass dataWrapper = new WrappingClass();

            JsonUtility.FromJsonOverwrite(jsonStringToLoad, dataWrapper);

            TableData tData = new TableData();

            tData = LoadDataToTableData(dataWrapper);

            return tData;
        } else {
            return new TableData();
        }
    }

    //Use the classes below to alter what is saved and what is loaded
    //Ensure that the order is aligned with that of UserData class
    private static WrappingClass SaveDataFromTableData(TableData tData) {
        WrappingClass dataWrapper = new WrappingClass();

        dataWrapper._userLoginData.Add(tData.username);
        dataWrapper._userLoginData.Add(tData.password);

        return dataWrapper;
    }

    private static TableData LoadDataToTableData(WrappingClass dataWrapper) {
        TableData tData = new TableData();

        tData.username = dataWrapper._userLoginData[0];
        tData.password = dataWrapper._userLoginData[1];

        return tData;
    }
}