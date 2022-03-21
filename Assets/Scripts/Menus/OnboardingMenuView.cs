using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnboardingMenuView : View {

    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _affirmationButton;
    [SerializeField] private List<Toggle> _notificationToggles;

    private Toggle _currentToggle;

    public override void Initialise() {
        _continueButton.onClick.AddListener(() => ViewManager.Show<MainMenuView>(false));
        _affirmationButton.onClick.AddListener(() => ViewManager.Show<AffirmationMenuView>());

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
