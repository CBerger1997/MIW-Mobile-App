using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUpMenuView : View {

    [SerializeField] private Text _welcomeText;
    [SerializeField] private Text _quoteText;
    [SerializeField] private Text _affirmationText;

    Animator _animator;

    public override void Initialise() {
        _welcomeText.gameObject.SetActive(true);
        _quoteText.gameObject.SetActive(false);
        _affirmationText.gameObject.SetActive(false);

        _animator = GetComponent<Animator>();
    }

    private void Update() {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("StartUpAnim")) {
            ViewManager.Show<OnboardingMenuView>(false);
        }
    }
}