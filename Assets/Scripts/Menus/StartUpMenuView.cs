using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartUpMenuView : View {

    [SerializeField] private List<TextMeshProUGUI> _firstTimeStartUp;
    [SerializeField] private List<TextMeshProUGUI> _startUpPage;
    [SerializeField] private GameObject _permanentMenu;


    Animator _animator;

    bool isStartUp = false;
    bool isUserTouchingScreen = false;

    public override void Initialise() {
        SetQuoteText();

        SetAffirmationText();

        if (!AppManager.instance._uData.hasOnboarded) {
            foreach (TextMeshProUGUI text in _firstTimeStartUp) {
                text.gameObject.SetActive(true);
            }

            foreach (TextMeshProUGUI text in _startUpPage) {
                text.gameObject.SetActive(false);
            }
        } else {
            foreach (TextMeshProUGUI text in _firstTimeStartUp) {
                text.gameObject.SetActive(false);
            }

            foreach (TextMeshProUGUI text in _startUpPage) {
                text.gameObject.SetActive(true);
            }
        }
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && !isUserTouchingScreen) {
            isUserTouchingScreen = true;

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("StartUpAnim")) {
                _animator.Play("StaticState");
            } else {
                if (!AppManager.instance._uData.hasOnboarded) {
                    ViewManager.Show<OnboardingMenuView>(false);
                } else if (!AppManager.instance._uData.hasUserCheckedIn) {
                    _permanentMenu.SetActive(true);
                    ViewManager.Show<CheckInMenuView>(false);
                } else {
                    _permanentMenu.SetActive(true);
                    ViewManager.Show<MainMenuView>(false);
                }
            }

        } else if (Input.touchCount == 0) {
            isUserTouchingScreen = false;
        }
    }

    private void SetQuoteText() {
        int randomNum = Random.Range(0, AppManager.instance._qManager.author.Count - 1);

        string text = AppManager.instance._qManager.quote[randomNum] +
            " - " +
            AppManager.instance._qManager.author[randomNum];

        _firstTimeStartUp[1].text = text;
        _startUpPage[0].text = text;
    }

    private void SetAffirmationText() {
        string text = "";

        switch (AppManager.instance._uData.userAffirmationSelection) {
            case 0:
                int randomNum = Random.Range(0, AppManager.instance._aManager.affirmationRandom.Count - 1);
                text = AppManager.instance._aManager.affirmationRandom[randomNum];
                break;
            case 1:
                text = AppManager.instance._aManager.affirmationSelection[AppManager.instance._uData.userAffirmationListSelection];
                break;
            case 2:
                text = "I am " + AppManager.instance._uData.userAffirmationPersonalSelection;
                break;
        }

        _startUpPage[1].text = text;

    }
}