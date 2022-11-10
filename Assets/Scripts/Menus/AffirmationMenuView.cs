using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class AffirmationMenuView : View {
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _helpButton;

    [SerializeField] private TextMeshProUGUI _infoText;

    [SerializeField] private GameObject _toggleScreen;
    [SerializeField] private GameObject _helpScreen;

    [SerializeField] private GameObject _listContentParent;
    [SerializeField] private GameObject _listContent;
    [SerializeField] private GameObject _personalContent;
    [SerializeField] private InputField _personalInputText;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private GameObject _toggleContent;

    private int _currentOptionSelection;
    private int _currentListSelection;
    private GameObject _listButton;

    public override void Initialise() {
        _backButton.onClick.AddListener(delegate { MoveToPreviousScreen(); });
        _helpButton.onClick.AddListener(delegate { ToggleHelpMenu(); });

        _toggleContent.GetComponent<ScrollSwipe>().OnSelectionChange += OnSelectionChangeHandler;
        _currentOptionSelection = AppManager.instance._uData.userAffirmationSelection;
        _personalInputText.text = AppManager.instance._uData.userAffirmationPersonalSelection;

        _toggleScreen.SetActive(true);
        _helpScreen.SetActive(false);

        SetAffirmationInfoTextAndContent();
        SetupAffirmationList();
    }

    private void OnSelectionChangeHandler() {
        int val = _toggleContent.GetComponent<ScrollSwipe>().selection;

        if (val != _currentOptionSelection) {
            _currentOptionSelection = val;
            SetAffirmationInfoTextAndContent();
        }
    }

    private void SetAffirmationInfoTextAndContent() {
        switch (_currentOptionSelection) {
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
                _infoText.text = "Create your own affirmation starting from \"You are\"";
                _listContentParent.SetActive(false);
                _personalContent.SetActive(true);
                break;
        }
    }

    private void SetupAffirmationList() {
        for (int i = 0; i < AppManager.instance._aManager.affirmationSelection.Count - 1; i++) {
            GameObject affirmationButton = Instantiate(_buttonPrefab, _listContent.gameObject.transform);
            affirmationButton.name = i.ToString();
            affirmationButton.GetComponentInChildren<Text>().text = AppManager.instance._aManager.affirmationSelection[i];
            affirmationButton.GetComponent<Button>().onClick.AddListener(delegate { OnListAffirmationClicked(int.Parse(affirmationButton.name), affirmationButton); });

            if (AppManager.instance._uData.userAffirmationListSelection >= 0) {
                if (i == AppManager.instance._uData.userAffirmationListSelection) {
                    _listButton = affirmationButton;
                    _listButton.GetComponent<Image>().color = Color.green;
                }
            }
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
            SaveAffirmationData();
            ViewManager.ShowLast();
        } else if (_helpScreen.activeSelf == true) {
            _toggleScreen.SetActive(true);
            _helpScreen.SetActive(false);
        }
    }

    private void OnListAffirmationClicked(int index, GameObject button) {


        if (_listButton != null) {
            _listButton.GetComponent<Image>().color = Color.white;
        }

        _currentListSelection = index;
        _listButton = button;
        _listButton.GetComponent<Image>().color = Color.green;
    }

    private void SaveAffirmationData() {

        AppManager.instance._uData.userAffirmationSelection = _currentOptionSelection;
        AppManager.instance._uData.userAffirmationPersonalSelection = "";
        AppManager.instance._uData.userAffirmationListSelection = -1;

        switch (_currentOptionSelection) {
            case 1:
                AppManager.instance._uData.userAffirmationListSelection = _currentListSelection;
                break;
            case 2:
                AppManager.instance._uData.userAffirmationPersonalSelection = _personalInputText.text;
                break;
        }

        AppManager.instance.SaveUserData();
    }
}