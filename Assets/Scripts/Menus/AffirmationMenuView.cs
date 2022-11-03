using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

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
    private UnityEvent _affirmationChangeEvent = new UnityEvent();

    public override void Initialise() {
        _backButton.onClick.AddListener(delegate { MoveToPreviousScreen(); });
        _helpButton.onClick.AddListener(delegate { ToggleHelpMenu(); });

        _toggleContent.GetComponent<ScrollSwipe>().OnSelectionChange += OnSelectionChangeHandler;

        _currentSelection = 0;

        _infoText.text = "Provides you with a different affirmation every day";
        _listContentParent.SetActive(false);
        _personalContent.SetActive(false);

        SetupAffirmationList();

        _toggleScreen.SetActive(true);
        _helpScreen.SetActive(false);
    }

    private void OnSelectionChangeHandler() {
        SetAffirmationInfoTextAndContent();
    }

    private void SetAffirmationInfoTextAndContent() {

        int val = _toggleContent.GetComponent<ScrollSwipe>().selection;

        if (val != _currentSelection) {
            _currentSelection = val;

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
                    _infoText.text = "Create your own affirmation starting from \"You are\"";
                    _listContentParent.SetActive(false);
                    _personalContent.SetActive(true);
                    break;
            }
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