using UnityEngine;

public class AppManager : MonoBehaviour {

    public static AppManager instance = null;

    public UserData _uData { get; set; }

    public TableData _tData { get; set; }

    public static AppManager Instance {
        get { return instance; }
    }

    private void Awake() {
        // Check for singleton instance and set or destory the instance accordingly
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);

        LoadUserData();

        LoadTableData();
    }

    public void SaveUserData() {
        SaveAndLoadUserData.SaveUserData(_uData);
    }

    public void LoadUserData() {
        _uData = SaveAndLoadUserData.LoadUserData();
    }

    public void SaveTableData() {
        SaveAndLoadTableData.SaveTableData(_tData);
    }

    public void LoadTableData() {
        _tData = SaveAndLoadTableData.LoadTableData();
    }

}
