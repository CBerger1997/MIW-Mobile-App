using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JustBreatheSetupView : View
{
    [SerializeField] private Button _startButton;
    [SerializeField] private TMP_Dropdown _sessionDropdown;
    [SerializeField] private Toggle _yesToggle;
    [SerializeField] private Toggle _noToggle;
    [SerializeField] private Toggle _visualToggle;
    [SerializeField] private Toggle _voicedToggle;

    public TMP_Dropdown BreathingDropdown;

    private HelpScreen _helpScreen;

    public override void Initialise ()
    {
        _helpScreen = this.GetComponent<HelpScreen> ();
        _helpScreen.ConfigureHelpScreen ();

        CalibrateSessionDropdown ();
        CalibrateBreathingDropdown ();

        _yesToggle.onValueChanged.AddListener ( delegate
        { OnToggleTriggered ( _yesToggle, _noToggle ); } );

        _noToggle.onValueChanged.AddListener ( delegate
        { OnToggleTriggered ( _noToggle, _yesToggle ); } );

        _voicedToggle.onValueChanged.AddListener ( delegate
        { OnToggleTriggered ( _voicedToggle, _visualToggle ); } );

        _visualToggle.onValueChanged.AddListener ( delegate
        { OnToggleTriggered ( _visualToggle, _voicedToggle ); } );

        _startButton.onClick.AddListener ( OnStartClicked );
    }

    public override void Show ()
    {
        base.Show ();

        _helpScreen.ToggleOffHelpMenu ();
    }

    private void CalibrateSessionDropdown ()
    {
        List<string> minuteList = new List<string> ();

        for ( int i = 1; i < 11; i++ )
        {
            string minuteString = i > 1 ? " Minutes" : " Minute";
            minuteList.Add ( i.ToString () + minuteString );
        }

        _sessionDropdown.ClearOptions ();
        _sessionDropdown.AddOptions ( minuteList );
    }

    private void CalibrateBreathingDropdown ()
    {
        List<string> breathingList = new List<string>
        {
            "4 seconds in; 7 seconds out",
            "5 seconds in; 9 seconds out",
            "6 seconds in; 10 seconds out"
        };

        BreathingDropdown.ClearOptions ();
        BreathingDropdown.AddOptions ( breathingList );
    }

    private void OnToggleTriggered ( Toggle triggeredToggle, Toggle affectedToggle )
    {
        if ( triggeredToggle.isOn )
        {
            affectedToggle.isOn = false;
        }
        else if ( !affectedToggle.isOn )
        {
            triggeredToggle.isOn = true;
        }
    }

    private void OnStartClicked ()
    {
        ViewManager.Show<JustBreathePlayerView> ();
    }
}
