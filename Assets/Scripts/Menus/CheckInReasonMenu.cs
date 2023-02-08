using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInReasonMenu : View {
    [SerializeField] private List<Button> _contextButtons = new List<Button>();
    [SerializeField] private Button _iDontKnowButton;
    [SerializeField] private Button _saveButton;

    public override void Initialise() {
        for (int i = 0; i < _contextButtons.Count; i++) {
            int copy = i;
            _contextButtons[copy].onClick.AddListener(delegate { OnClickContextSelected(copy); });
        }

        _iDontKnowButton.onClick.AddListener(delegate { OnIDKButtonClicked(); });

        _saveButton.onClick.AddListener(delegate { MoveToNextScreen(2); });
        _saveButton.interactable = false;
    }

    private void OnClickContextSelected(int val) {
        AppManager.instance._uData.currentContextValue = val;
        _saveButton.interactable = true;
    }
    private void OnIDKButtonClicked() {
        AppManager.instance._uData.currentContextValue = -1;
        _saveButton.interactable = true;
    }

    private void MoveToNextScreen(int val) {
        SaveCheckInData();

        AppManager.instance._tData._checkInData.Add(new TableData.CheckInClass(AppManager.instance._uData.currentEmotionValue, AppManager.instance._uData.currentContextValue, DateTime.Now.ToString()));

        ViewManager.Show<MainMenuView>(false);
    }

    private void SaveCheckInData() {
        AppManager.instance._uData.SaveCheckInData();
        AppManager.instance.SaveUserData();
    }
}
