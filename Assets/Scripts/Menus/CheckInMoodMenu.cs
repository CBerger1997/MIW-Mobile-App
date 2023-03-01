using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInMoodMenu : View {
    [SerializeField] private List<Button> _emotionButtons = new List<Button>();
    [SerializeField] private Button _iDontKnowButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    [SerializeField] private Button _journalButton;
    [SerializeField] private Button _checkInAnalysisButton;

    [SerializeField] private ScrollRect _emotionsRect;

    private Button _selectedButton;

    public override void Initialise() {

        for (int i = 0; i < _emotionButtons.Count; i++) {
            int copy = i;
            _emotionButtons[copy].onClick.AddListener(() => OnClickEmotionSelected(copy, _emotionButtons[copy]));
        }

        _iDontKnowButton.onClick.AddListener(OnIDKButtonClicked);

        _leftButton.onClick.AddListener(OnLeftClicked);
        _rightButton.onClick.AddListener(OnRightClicked);

        _saveButton.onClick.AddListener(() => ViewManager.Show<CheckInReasonMenu>());
        _saveButton.interactable = false;

        _journalButton.onClick.AddListener(() => ViewManager.Show<JournalMenuView>());
        _checkInAnalysisButton.onClick.AddListener(() => ViewManager.Show<CheckInAnalysisMenuView>());

        _emotionsRect.horizontalNormalizedPosition = 0.5f;
    }

    private void OnClickEmotionSelected(int val, Button button) {
        AppManager.instance._uData.currentEmotionValue = val;
        _saveButton.interactable = true;

        if(_selectedButton != null) {
            _selectedButton.GetComponent<Image>().color = Color.white;
        }

        _selectedButton = button;
        _selectedButton.GetComponent<Image>().color = new Color32(139, 210, 235, 255);
    }

    private void OnIDKButtonClicked() {
        AppManager.instance._uData.currentEmotionValue = -1;
        _saveButton.interactable = true;
    }

    private void OnLeftClicked() {
        _emotionsRect.horizontalNormalizedPosition -= 0.5f;
    }

    private void OnRightClicked() {
        _emotionsRect.horizontalNormalizedPosition += 0.5f;
    }
}
