using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInMenuView : View {
    [SerializeField] private GameObject _screen1;
    [SerializeField] private GameObject _screen2;

    [SerializeField] private List<Button> _emotionButtons = new List<Button>();
    [SerializeField] private List<Button> _contextButtons = new List<Button>();
    [SerializeField] private List<Button> _backButtons = new List<Button>();
    [SerializeField] private Button _checkinButton;

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


        for (int i = 0; i < _backButtons.Count; i++) {
            _backButtons[i].onClick.AddListener(delegate { MoveToPreviousScreen(i); });
        }
    }

    private void MoveToNextScreen(int val) {
        if (_screen1.activeSelf == true) {
            emotionalValue = val;

            _screen1.SetActive(false);
            _screen2.SetActive(true);
        } else if (_screen2.activeSelf == true) {
            contextValue = val;
            
            ResetUI();
            _checkinButton.GetComponent<Image>().color = Color.green;
            _checkinButton.enabled = false;
            ViewManager.Show<MainMenuView>(false);
        }
    }

    private void MoveToPreviousScreen(int val) {
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
}
