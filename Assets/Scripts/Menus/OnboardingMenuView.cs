using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnboardingMenuView : View {

    [SerializeField] private Button _continueButton;

    public override void Initialise() {
        _continueButton.onClick.AddListener(() => ViewManager.Show<MainMenuView>(false));
    }

}
