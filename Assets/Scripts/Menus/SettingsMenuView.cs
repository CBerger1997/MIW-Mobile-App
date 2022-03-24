using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuView : View {

    [SerializeField] private Button _notificationsButton;
    [SerializeField] private Button _declarationsButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _deleteUserButton;
    [SerializeField] private List<Toggle> _notificationToggles;

    [SerializeField] private GameObject _mainScreen;
    [SerializeField] private GameObject _notificationsScreen;
    [SerializeField] private GameObject _declarationsScreen;

    private Toggle _currentToggle;

    public override void Initialise() {
        _backButton.onClick.AddListener(delegate { BackButtonOnClick(); });
        _deleteUserButton.onClick.AddListener(delegate { DeleteUserDataOnClick(); });
        _notificationsButton.onClick.AddListener(delegate { ShowNotificationsScreen(); });
        _declarationsButton.onClick.AddListener(delegate { ShowDeclarationsScreen(); });

        foreach (Toggle toggle in _notificationToggles) {
            toggle.onValueChanged.AddListener(delegate { OnToggleValueChange(toggle); });
        }

        _mainScreen.SetActive(true);
        _notificationsScreen.SetActive(false);
        _declarationsScreen.SetActive(false);
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

    private void ShowNotificationsScreen() {
        _mainScreen.SetActive(false);
        _notificationsScreen.SetActive(true);
        _declarationsScreen.SetActive(false);

        _currentToggle = AppManager.instance._uData.isNotificationsOn == true ? _notificationToggles[0] : _notificationToggles[1];
        _currentToggle.isOn = true;
    }

    private void ShowDeclarationsScreen() {
        _mainScreen.SetActive(false);
        _notificationsScreen.SetActive(false);
        _declarationsScreen.SetActive(true);
    }

    private void BackButtonOnClick() {
        if (_mainScreen.activeSelf) {
            AppManager.instance._uData.isNotificationsOn = _notificationToggles[0].isOn == true ? true : false;
            AppManager.instance.SaveUserData();
            ViewManager.ShowLast();
        } else {
            _mainScreen.SetActive(true);
            _notificationsScreen.SetActive(false);
            _declarationsScreen.SetActive(false);
        }
    }

    private void DeleteUserDataOnClick() {
        AppManager.instance._uData = new UserData();
        AppManager.instance.SaveUserData();
    }
}