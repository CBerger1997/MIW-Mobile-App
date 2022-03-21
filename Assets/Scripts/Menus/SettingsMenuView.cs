using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuView : View {

    [SerializeField] private Button _backButton;
    [SerializeField] private List<Toggle> _notificationToggles;

    private Toggle _currentToggle;

    public override void Initialise() {
        _backButton.onClick.AddListener(() => ViewManager.ShowLast());

        foreach (Toggle toggle in _notificationToggles) {
            toggle.onValueChanged.AddListener(delegate { OnToggleValueChange(toggle); });
        }

        _currentToggle = _notificationToggles[0];
        _currentToggle.isOn = true;
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
}