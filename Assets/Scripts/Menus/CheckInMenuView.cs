using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInMenuView : View {
    [SerializeField] private GameObject _screen1;
    [SerializeField] private GameObject _screen2;

    [SerializeField] private List<Button> _emotionButtons = new List<Button>();
    [SerializeField] private List<Button> _contextButtons = new List<Button>();
    [SerializeField] private List<Button> _iDontKnowButtons = new List<Button>();
    [SerializeField] private List<Button> _saveButtons = new List<Button>();

    [SerializeField] private Button _journalButton;
    [SerializeField] private Button _checkInAnalysisButton;

    [SerializeField] private List<Button> _backButtons = new List<Button>();
    [SerializeField] private ScrollRect _emotionsRect;

    public int emotionalValue { get; set; }
    public int contextValue { get; set; }

    public override void Initialise() {
        ResetUI();

        for (int i = 0; i < _emotionButtons.Count; i++) {
            int copy = i;
            _emotionButtons[copy].onClick.AddListener(delegate { OnClickEmotionSelected(copy); });
        }

        for (int i = 0; i < _contextButtons.Count; i++) {
            int copy = i;
            _contextButtons[copy].onClick.AddListener(delegate { OnClickContextSelected(copy); });
        }

        for (int i = 0; i < _iDontKnowButtons.Count; i++) {
            _iDontKnowButtons[i].onClick.AddListener(delegate { MoveToNextScreen(1); });
        }

        for (int i = 0; i < _saveButtons.Count; i++) {
            _saveButtons[i].onClick.AddListener(delegate { MoveToNextScreen(2); });
            _saveButtons[i].interactable = false;
        }

        for (int i = 0; i < _backButtons.Count; i++) {
            _backButtons[i].onClick.AddListener(delegate { MoveToPreviousScreen(); });
        }

        _journalButton.onClick.AddListener(delegate { ViewManager.Show<JournalMenuView>(true); });
        _checkInAnalysisButton.onClick.AddListener(delegate { ViewManager.Show<CheckInAnalysisMenuView>(true); });

        _emotionsRect.horizontalNormalizedPosition = 0.5f;
    }

    private void OnClickEmotionSelected(int val) {
        emotionalValue = val;
        _saveButtons[0].interactable = true;
    }

    private void OnClickContextSelected(int val) {
        contextValue = val;
        _saveButtons[1].interactable = true;
    }

    private void MoveToNextScreen(int val) {
        if (_screen1.activeSelf == true) {

            switch (val) {
                case 1:
                    //I don't know triggered
                    emotionalValue = -1;
                    break;
                case 2:
                    //Save triggered
                    break;
            }

            _screen1.SetActive(false);
            _screen2.SetActive(true);
        } else if (_screen2.activeSelf == true) {

            switch (val) {
                case 1:
                    //I don't know triggered
                    contextValue = -1;
                    break;
                case 2:
                    //Save triggered
                    break;
            }

            ResetUI();

            SaveCheckInData();

            AppManager.instance._tData._checkInData.Add(new TableData.CheckInClass(emotionalValue, contextValue, DateTime.Now.ToString()));

            ViewManager.Show<MainMenuView>(false);
        }
    }

    private void MoveToPreviousScreen() {
        if (_screen1.activeSelf == true) {
            ViewManager.Show<MainMenuView>(false);
        } else if (_screen2.activeSelf == true) {
            ResetUI();
        }
    }

    private void ResetUI() {
        _screen1.SetActive(true);
        _screen2.SetActive(false);

        _contextButtons[0].transform.parent.GetComponent<RectTransform>().position = Vector3.zero;
    }

    private void SaveCheckInData() {
        if (AppManager.instance._uData.emotionDates.Count > 0 &&
            System.DateTime.Now.ToString("yyyy/MM/dd") == AppManager.instance._uData.emotionDates[AppManager.instance._uData.emotionDates.Count - 1]) {
            AppManager.instance._uData.emotionValues[AppManager.instance._uData.emotionValues.Count - 1] = emotionalValue;
            AppManager.instance._uData.contextValues[AppManager.instance._uData.contextValues.Count - 1] = contextValue;
        } else {
            AppManager.instance._uData.emotionValues.Add(emotionalValue);
            AppManager.instance._uData.contextValues.Add(contextValue);
            AppManager.instance._uData.emotionDates.Add(System.DateTime.Now.ToString("yyyy/MM/dd"));
        }

        if (emotionalValue != -1 && contextValue != -1) {
            AppManager.instance._uData.hasUserCheckedIn = true;
        }

        AppManager.instance.SaveUserData();
    }
}
