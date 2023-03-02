using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JustBreathMenuView : View {
    [SerializeField] private AutoSpaceOnResolution _autoSpaceOnResolution;

    public override void Initialise() {
        _autoSpaceOnResolution.PerformAutoSpace();
    }
}
