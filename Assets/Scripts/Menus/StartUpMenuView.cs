using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartUpMenuView : View {

    [SerializeField] private List<TextMeshProUGUI> _startPageTexts1;
    [SerializeField] private List<TextMeshProUGUI> _startPageTexts2;

    Animator _animator;

    bool isStartUpStage2 = false;
    bool isUserTouchingScreen = false;

    public override void Initialise() {
        foreach (TextMeshProUGUI text in _startPageTexts1) {
            text.gameObject.SetActive(true);
        }

        foreach (TextMeshProUGUI text in _startPageTexts2) {
            text.gameObject.SetActive(false);
        }

        _animator = GetComponent<Animator>();
    }

    private void Update() {
        if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && !isUserTouchingScreen) {
            isUserTouchingScreen = true;

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("StartUpAnim")) {
                _animator.Play("StaticState");
            } else if (!isStartUpStage2) {
                isStartUpStage2 = true;

                foreach (TextMeshProUGUI text in _startPageTexts1) {
                    text.gameObject.SetActive(false);
                }

                foreach (TextMeshProUGUI text in _startPageTexts2) {
                    text.gameObject.SetActive(true);
                }

                _animator.Play("StartUpAnim");
            } else {
                if (!AppManager.instance._uData.hasOnboarded) {
                    ViewManager.Show<OnboardingMenuView>(false);
                } else if (!AppManager.instance._uData.hasUserCheckedIn) {
                    ViewManager.Show<CheckInMenuView>(false);
                } else {
                    ViewManager.Show<MainMenuView>(false);
                }
            }

        } else if (Input.touchCount == 0) {
            isUserTouchingScreen = false;
        }
    }
}