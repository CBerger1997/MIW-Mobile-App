using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AffirmationMenuView : View {
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _helpButton;

    [SerializeField] private List<Toggle> _affirmationToggles;
    [SerializeField] private TextMeshProUGUI _affirmationInfoText;

    [SerializeField] private GameObject _toggleScreen;
    [SerializeField] private GameObject _helpScreen;

    [SerializeField] private GameObject _listContent;
    [SerializeField] private GameObject _personalContent;

    private Toggle _currentToggle;

    public override void Initialise() {
        _backButton.onClick.AddListener(delegate { MoveToPreviousScreen(); });
        _helpButton.onClick.AddListener(delegate { ToggleHelpMenu(); });

        foreach (Toggle toggle in _affirmationToggles) {
            toggle.onValueChanged.AddListener(delegate { OnToggleValueChange(toggle); });
        }

        _currentToggle = _affirmationToggles[0];
        _currentToggle.isOn = true;

        _toggleScreen.SetActive(true);
        _helpScreen.SetActive(false);
    }

    private void OnToggleValueChange(Toggle toggle) {
        if (toggle.isOn) {
            _currentToggle = toggle;

            int tCount = 0;

            foreach (Toggle t in _affirmationToggles) {
                if (t != toggle) {
                    t.isOn = false;
                } else {
                    SetAffirmationInfoTextAndContent(tCount);
                }
                tCount++;
            }
        } else if (_currentToggle.isOn == false) {
            _currentToggle.isOn = true;
        }
    }

    private void SetAffirmationInfoTextAndContent(int val) {
        switch (val) {
            case 0:
                _affirmationInfoText.text = "Provides you with a different affirmation every day";
                _listContent.SetActive(false);
                _personalContent.SetActive(false);
                break;
            case 1:
                _affirmationInfoText.text = "Choose your affirmation from the list below";
                _listContent.SetActive(true);
                _personalContent.SetActive(false);
                break;
            case 2:
                _affirmationInfoText.text = "Create up your own affirmation starting from \"You are\"";
                _listContent.SetActive(false);
                _personalContent.SetActive(true);
                break;
        }
    }

    private void ToggleHelpMenu() {
        if(_helpScreen.activeSelf) {
            _toggleScreen.SetActive(true);
            _helpScreen.SetActive(false);
        } else {
            _toggleScreen.SetActive(false);
            _helpScreen.SetActive(true);
        }
    }

    private void MoveToPreviousScreen() {
        if (_toggleScreen.activeSelf == true) {
            ViewManager.ShowLast();
        } else if (_helpScreen.activeSelf == true) {
            _toggleScreen.SetActive(true);
            _helpScreen.SetActive(false);
        }
    }
}