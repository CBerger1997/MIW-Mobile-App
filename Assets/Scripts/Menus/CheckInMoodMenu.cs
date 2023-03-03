using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInMoodMenu : View {
    [SerializeField] private List<Button> _emotionButtons = new List<Button>();
    [SerializeField] private Button _iDontKnowButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _journalButton;
    [SerializeField] private Button _checkInAnalysisButton;
    [SerializeField] private ScrollRect _emotionsRect;
    [SerializeField] private GameObject _toggleContent;
    //[SerializeField] private AutoSpaceOnResolution _autoSpaceOnResolution;

    private Button _selectedButton;
    private int _currentOptionSelection;

    public override void Initialise() {

        for (int i = 0; i < _emotionButtons.Count; i++) {
            int copy = i;
            _emotionButtons[copy].onClick.AddListener(() => OnClickEmotionSelected(copy, _emotionButtons[copy]));
        }

        _iDontKnowButton.onClick.AddListener(OnIDKButtonClicked);

        _saveButton.onClick.AddListener(() => ViewManager.Show<CheckInReasonMenu>());
        _saveButton.interactable = false;

        _journalButton.onClick.AddListener(() => ViewManager.Show<JournalMenuView>());
        _checkInAnalysisButton.onClick.AddListener(() => ViewManager.Show<CheckInAnalysisMenuView>());

        _toggleContent.GetComponent<ScrollSwipe>().OnSelectionChange += OnSelectionChangeHandler;

        _emotionsRect.horizontalNormalizedPosition = 0.5f;

        _toggleContent.GetComponent<ScrollSwipe>().PresetPosition(2);

        //_autoSpaceOnResolution.PerformAutoSpace();
    }

    private void OnClickEmotionSelected(int val, Button button) {
        if(AppManager.instance._uData.currentEmotionValue == -1) {
            _iDontKnowButton.GetComponentInChildren<TMPro.TMP_Text>().color = Color.white;
        }

        AppManager.instance._uData.currentEmotionValue = val;
        _saveButton.interactable = true;

        if(_selectedButton != null) {
            _selectedButton.GetComponent<Image>().color = Color.white;
        }

        _selectedButton = button;
        _selectedButton.GetComponent<Image>().color = new Color32(139, 210, 235, 255);
    }

    private void OnIDKButtonClicked() {
        if (_selectedButton != null) {
            _selectedButton.GetComponent<Image>().color = Color.white;
        }

        _iDontKnowButton.GetComponentInChildren<TMPro.TMP_Text>().color = Color.grey;
        AppManager.instance._uData.currentEmotionValue = -1;
        _saveButton.interactable = true;
    }

    private void OnSelectionChangeHandler() {
        int val = _toggleContent.GetComponent<ScrollSwipe>().selection;

        if (val != _currentOptionSelection) {
            _currentOptionSelection = val;
        }
    }
}
