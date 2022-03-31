using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuView : View {

    [SerializeField] private Button _notificationsButton;
    [SerializeField] private Button _declarationsButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _deleteUserButton;
    [SerializeField] private Button _changePasswordButton;
    [SerializeField] private Button _saveButton;

    [SerializeField] private List<Toggle> _notificationToggles;

    [SerializeField] private List<GameObject> _screens;

    [SerializeField] private GameObject _warningText;

    [SerializeField] private TMP_InputField _curPasswordInputField;
    [SerializeField] private TMP_InputField _newPasswordInputField;
    [SerializeField] private TMP_InputField _newCopyPasswordInputField;

    private Toggle _currentToggle;

    public override void Initialise() {
        _backButton.onClick.AddListener(delegate { BackButtonOnClick(); });
        _saveButton.onClick.AddListener(delegate { SaveButtonOnClick(); });
        _deleteUserButton.onClick.AddListener(delegate { DeleteUserDataOnClick(); });
        _notificationsButton.onClick.AddListener(delegate { ShowNotificationsScreen(1); });
        _declarationsButton.onClick.AddListener(delegate { ShowSpecificScreen(2); });
        _changePasswordButton.onClick.AddListener(delegate { ShowPasswordScreen(3); });

        foreach (Toggle toggle in _notificationToggles) {
            toggle.onValueChanged.AddListener(delegate { OnToggleValueChange(toggle); });
        }

        ShowSpecificScreen(0);
    }

    private void OnToggleValueChange(Toggle toggle) {
        if (toggle.isOn) {
            _currentToggle = toggle;
            foreach (Toggle t in _notificationToggles) {
                if (t != toggle) {
                    t.isOn = false;
                }
            }
        } else if (_currentToggle.isOn == false) {
            _currentToggle.isOn = true;
        }
    }

    private void ShowNotificationsScreen(int index) {
        ShowSpecificScreen(index);

        _currentToggle = AppManager.instance._uData.isNotificationsOn == true ? _notificationToggles[0] : _notificationToggles[1];
        _currentToggle.isOn = true;
    }

    private void ShowPasswordScreen(int index) {
        ShowSpecificScreen(index);

        _warningText.SetActive(false);
    }

    private void ShowSpecificScreen(int index) {
        for (int i = 0; i < _screens.Count; i++) {
            if(i == index) {
                _screens[i].SetActive(true);
            } else {
                _screens[i].SetActive(false);
            }
        }
    }

    private void SaveButtonOnClick() {
        if(_curPasswordInputField.text == AppManager.instance._uData.password) {
            if(_newPasswordInputField.text == _newCopyPasswordInputField.text) {
                AppManager.instance._uData.password = _newPasswordInputField.text;
                AppManager.instance.SaveUserData();
                AppManager.instance._tData.password = _newPasswordInputField.text;
                AppManager.instance.SaveTableData();
                ShowSpecificScreen(0);
            } else {
                _warningText.GetComponent<TextMeshProUGUI>().text = "Your new passwords don't match";
                _warningText.SetActive(true);
            }
        } else {
            _warningText.GetComponent<TextMeshProUGUI>().text = "Your current password is incorrect";
            _warningText.SetActive(true);
        }
    }

    private void BackButtonOnClick() {
        if (_screens[0].activeSelf) {
            AppManager.instance._uData.isNotificationsOn = _notificationToggles[0].isOn == true ? true : false;
            AppManager.instance.SaveUserData();
            ViewManager.ShowLast();
        } else {
            ShowSpecificScreen(0);
        }
    }

    private void DeleteUserDataOnClick() {
        AppManager.instance._uData = new UserData();
        AppManager.instance.SaveUserData();
    }
}