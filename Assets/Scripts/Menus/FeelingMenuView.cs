using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeelingMenuView : View {
    [SerializeField] private GameObject _screen1;
    [SerializeField] private GameObject _screen2;
    [SerializeField] private GameObject _screen3;

    [SerializeField] private List<Button> _emotionButtons = new List<Button>();
    [SerializeField] private List<Button> _contextButtons = new List<Button>();

    [SerializeField] private Button _managerYesButton;
    [SerializeField] private Button _managerNoButton;
    [SerializeField] private Button _journalYesButton;
    [SerializeField] private Button _journalNoButton;

    [SerializeField] private GameObject _managerUI;
    [SerializeField] private InputField _managerEmailInputField;

    private bool _isShow = false;

    public int emotionalValue { get; set; }
    public int contextValue { get; set; }


    public override void Initialise() {
        ResetUI();


        for (int i = 0; i < _emotionButtons.Count; i++) {
            _emotionButtons[i].onClick.AddListener(delegate { MoveToNextScreen(i); });
        }

        for (int i = 0; i < _contextButtons.Count; i++) {
            _contextButtons[i].onClick.AddListener(delegate { MoveToNextScreen(i); });
        }

        _managerYesButton.onClick.AddListener(delegate { CheckManagerUI(true); });
        _managerNoButton.onClick.AddListener(delegate { CheckManagerUI(false); });

        _journalYesButton.onClick.AddListener(delegate { MoveToNextScreen(1); });
        _journalNoButton.onClick.AddListener(delegate { MoveToNextScreen(0); });

    }

    private void MoveToNextScreen(int val) {
        if (_screen1.activeSelf == true) {
            _screen1.SetActive(false);
            _screen2.SetActive(true);
            emotionalValue = val;
        } else if (_screen2.activeSelf == true) {
            _screen2.SetActive(false);
            _screen3.SetActive(true);
            contextValue = val;
        } else if (_screen3.activeSelf == true) {
            ResetUI();

            if(_isShow) {
                //TODO: Sort out sending manager email
            }

            if(val == 0) {
                ViewManager.ShowLast();
            } else {
                //TODO: Uncomment once journal view is implemented
                //ViewManager.Show<JournalMenuView>();
                //Remove below once above is implemented
                ViewManager.ShowLast();
            }
        }
    }

    private void CheckManagerUI(bool isShow) {
        _isShow = isShow;

        _managerNoButton.gameObject.SetActive(false);
        _managerYesButton.gameObject.SetActive(false);

        if(isShow) {
            _managerUI.SetActive(true);
        }
    }

    private void ResetUI() {
        _screen1.SetActive(true);
        _screen2.SetActive(false);
        _screen3.SetActive(false);

        _managerNoButton.gameObject.SetActive(true);
        _managerYesButton.gameObject.SetActive(true);
        _managerUI.SetActive(false);
        _managerEmailInputField.text = "";
    }
}
