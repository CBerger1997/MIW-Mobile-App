using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfCommitmentMenuView : View {
    private HelpScreen _helpScreen;

    public override void Initialise () {
        _helpScreen = this.GetComponent<HelpScreen> ();
        _helpScreen.ConfigureHelpScreen ();
    }

    public override void Show () {
        base.Show ();

        _helpScreen.ToggleOffHelpMenu ();
    }
}
