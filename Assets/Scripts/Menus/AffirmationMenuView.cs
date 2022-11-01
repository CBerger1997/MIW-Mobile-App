using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AffirmationMenuView : View {
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _helpButton;

    [SerializeField] private Scrollbar _topScrollBar;
    [SerializeField] private TextMeshProUGUI _infoText;

    [SerializeField] private GameObject _toggleScreen;
    [SerializeField] private GameObject _helpScreen;

    [SerializeField] private GameObject _listContentParent;
    [SerializeField] private GameObject _listContent;
    [SerializeField] private GameObject _personalContent;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private GameObject _toggleContent;

    private int _currentSelection;

    public override void Initialise() {
        _backButton.onClick.AddListener(delegate { MoveToPreviousScreen(); });
        _helpButton.onClick.AddListener(delegate { ToggleHelpMenu(); });

        _topScrollBar.onValueChanged.AddListener(delegate { OnBarValueChange(); });

        SetupAffirmationList();

        _toggleScreen.SetActive(true);
        _helpScreen.SetActive(false);
    }

    private void OnBarValueChange() {
        SetAffirmationInfoTextAndContent();
    }

    private void SetAffirmationInfoTextAndContent() {

        int val = _toggleContent.GetComponent<ScrollSwipe>().selection;

        switch (val) {
            case 0:
                _infoText.text = "Provides you with a different affirmation every day";
                _listContentParent.SetActive(false);
                _personalContent.SetActive(false);
                break;
            case 1:
                _infoText.text = "Choose your affirmation from the list below";
                _listContentParent.SetActive(true);
                _personalContent.SetActive(false);
                break;
            case 2:
                _infoText.text = "Create up your own affirmation starting from \"You are\"";
                _listContentParent.SetActive(false);
                _personalContent.SetActive(true);
                break;
        }
    }

    private void SetupAffirmationList() {
        for (int i = 0; i < AppManager.instance._aManager.affirmationSelection.Count - 1; i++) {
            GameObject affirmationButton = Instantiate(_buttonPrefab, _listContent.gameObject.transform);
            affirmationButton.GetComponentInChildren<Text>().text = AppManager.instance._aManager.affirmationSelection[i];
        }
    }

    private void ToggleHelpMenu() {
        if (_helpScreen.activeSelf) {
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