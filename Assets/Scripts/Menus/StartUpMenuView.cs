using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartUpMenuView : View {
    [SerializeField] private List<GameObject> _firstTimeStartUp;
    [SerializeField] private List<GameObject> _startUpPage;
    [SerializeField] private GameObject _permanentMenu;
    [SerializeField] private GameObject _startUpParent1;
    [SerializeField] private GameObject _startUpParent2;
    [SerializeField] private Button _continueButton1;
    [SerializeField] private Button _continueButton2;

    Animator _animator;

    bool isUserTouchingScreen = false;

    public override void Initialise() {

        _continueButton1.onClick.AddListener(delegate { ContinueButtonClicked(); });
        _continueButton2.onClick.AddListener(delegate { ContinueButtonClicked(); });

        SetQuoteText();

        SetAffirmationText();

        _startUpParent1.SetActive(false);
        _startUpParent2.SetActive(false);

        if (!AppManager.instance._uData.hasOnboarded) {
            _startUpParent1.SetActive(true);
            _startUpParent2.SetActive(false);
        } else {
            _startUpParent1.SetActive(false);
            _startUpParent2.SetActive(true);
        }
        _animator = GetComponent<Animator>();

        Canvas.ForceUpdateCanvases();
    }

    public override void Show() {
        base.Show();
    }

    public override void Hide() {
        base.Hide();
    }

    private void Update() {
        if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && !isUserTouchingScreen) {
            isUserTouchingScreen = true;

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("StartUpAnim")) {
                _animator.Play("StaticState");
            }
        } else if (Input.touchCount == 0) {
            isUserTouchingScreen = false;
        }
    }

    private void SetQuoteText() {
        int randomNum = Random.Range(0, AppManager.instance._qManager.author.Count - 1);

        string text = AppManager.instance._qManager.quote[randomNum] +
            "\n \n" +
            AppManager.instance._qManager.author[randomNum];

        _firstTimeStartUp[1].GetComponent<TMP_Text>().text = text;
        _startUpPage[0].GetComponent<TMP_Text>().text = text;
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

        _startUpPage[1].GetComponent<TMP_Text>().text = text;
    }

    private void ContinueButtonClicked() {
        if (!AppManager.instance._uData.hasOnboarded) {
            ViewManager.Show<OnboardingMenuView>(false);
        } else if (!AppManager.instance._uData.hasUserCheckedIn) {
            _permanentMenu.SetActive(true);
            ViewManager.Show<CheckInMoodMenu>(false);
        } else {
            _permanentMenu.SetActive(true);
            ViewManager.Show<MainMenuView>(false);
        }
    }
}