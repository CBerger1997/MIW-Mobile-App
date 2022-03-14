using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JustBreathMenuView : View {

    [SerializeField] private Button _backButton;

    public override void Initialise() {
        _backButton.onClick.AddListener(() => ViewManager.ShowLast());
    }
}
