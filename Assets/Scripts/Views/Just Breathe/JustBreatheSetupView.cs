using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JustBreatheSetupView : View
{
    [SerializeField] private Button _startButton;
    [SerializeField] private TMP_Dropdown _sessionDropdown;
    [SerializeField] private TMP_Dropdown _breathingDropdown;
    [SerializeField] private Toggle _yesToggle;
    [SerializeField] private Toggle _noToggle;
    [SerializeField] private Toggle _visualToggle;
    [SerializeField] private Toggle _voicedToggle;
    [SerializeField] private Toggle _maleToggle;
    [SerializeField] private Toggle _femaleToggle;
    [SerializeField] private GameObject _genderVoiceSelection;
    [SerializeField] private JustBreathePlayerView _playerView;

    bool yesNoSelected = false;
    bool voicedVisualSelected = false;
    bool maleFemaleSelected = false;
    bool isOpening = false;

    public AudioClip[] _audioFilesMale;
    public AudioClip[] _audioFilesFemale;

    private HelpScreen _helpScreen;

    public override void Initialise ()
    {
        _helpScreen = this.GetComponent<HelpScreen> ();
        _helpScreen.ConfigureHelpScreen ();

        CalibrateSessionDropdown ();
        CalibrateBreathingDropdown ();

        _yesToggle.onValueChanged.AddListener ( delegate
        { OnToggleTriggered ( _yesToggle, _noToggle, "YesNo" ); } );

        _noToggle.onValueChanged.AddListener ( delegate
        { OnToggleTriggered ( _noToggle, _yesToggle, "YesNo" ); } );

        _voicedToggle.onValueChanged.AddListener ( delegate
        { OnToggleTriggered ( _voicedToggle, _visualToggle, "VoicedVisual" ); } );

        _visualToggle.onValueChanged.AddListener ( delegate
        { OnToggleTriggered ( _visualToggle, _voicedToggle, "VoicedVisual" ); } );

        _voicedToggle.onValueChanged.AddListener ( delegate
        { OnVoicedToggleTriggered ( true ); } );

        _visualToggle.onValueChanged.AddListener ( delegate
        { OnVoicedToggleTriggered ( false ); } );

        _maleToggle.onValueChanged.AddListener ( delegate
        { OnToggleTriggered ( _maleToggle, _femaleToggle, "MaleFemale" ); } );

        _femaleToggle.onValueChanged.AddListener ( delegate
        { OnToggleTriggered ( _femaleToggle, _maleToggle, "MaleFemale" ); } );

        _startButton.onClick.AddListener ( OnStartClicked );
    }

    public override void Show ()
    {
        isOpening = true;

        _helpScreen.ToggleOffHelpMenu ();

        _yesToggle.isOn = true;
        _noToggle.isOn = true;
        _voicedToggle.isOn = true;
        _visualToggle.isOn = true;
        _maleToggle.isOn = true;
        _femaleToggle.isOn = true;

        _yesToggle.isOn = false;
        _noToggle.isOn = false;
        _voicedToggle.isOn = false;
        _visualToggle.isOn = false;
        _maleToggle.isOn = false;
        _femaleToggle.isOn = false;

        _sessionDropdown.value = 0;
        _breathingDropdown.value = 0;

        _genderVoiceSelection.SetActive ( false );

        _startButton.interactable = false;

        yesNoSelected = false;
        voicedVisualSelected = false;
        maleFemaleSelected = false;

        isOpening = false;

        base.Show ();
    }

    private void CalibrateSessionDropdown ()
    {
        List<string> minuteList = new List<string>
        {
            "3 Minutes",
            "5 Minutes",
            "10 Minutes"
        };

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

        _breathingDropdown.ClearOptions ();
        _breathingDropdown.AddOptions ( breathingList );
    }

    private void OnToggleTriggered ( Toggle triggeredToggle, Toggle affectedToggle, string toggleGroup )
    {
        if ( !isOpening )
        {
            if ( triggeredToggle.isOn )
            {
                affectedToggle.isOn = false;
            }
            else if ( !affectedToggle.isOn )
            {
                triggeredToggle.isOn = true;
            }

            if ( toggleGroup == "YesNo" )
            {
                yesNoSelected = true;
            }
            else if ( toggleGroup == "VoicedVisual" )
            {
                voicedVisualSelected = true;
            }
            else if ( toggleGroup == "MaleFemale" )
            {
                maleFemaleSelected = true;
            }

            if ( yesNoSelected && voicedVisualSelected && _visualToggle.isOn )
            {
                _startButton.interactable = true;
            }
            else if ( yesNoSelected && voicedVisualSelected && maleFemaleSelected )
            {
                _startButton.interactable = true;
            }
            else
            {
                _startButton.interactable = false;
            }
        }
    }

    private void OnVoicedToggleTriggered ( bool isVoiced )
    {
        if ( isVoiced )
        {
            _genderVoiceSelection.SetActive ( true );
        }
        else
        {
            _genderVoiceSelection.SetActive ( false );
        }
    }

    private void OnStartClicked ()
    {
        if ( _voicedToggle.isOn )
        {
            if ( _maleToggle.isOn )
            {
                _playerView.SetAudioClip ( _audioFilesMale[ _breathingDropdown.value ] );
            }
            else
            {
                _playerView.SetAudioClip ( _audioFilesFemale[ _breathingDropdown.value ] );
            }
        }

        switch ( _breathingDropdown.value )
        {
            case 0:
                _playerView.SetInAndOut ( 4f, 7f );

                switch ( _sessionDropdown.value )
                {
                    case 0:
                        _playerView.SetBreathingIterations ( 15 );
                        break;
                    case 1:
                        _playerView.SetBreathingIterations ( 24 );
                        break;
                    case 2:
                        _playerView.SetBreathingIterations ( 45 );
                        break;
                }
                break;
            case 1:
                _playerView.SetInAndOut ( 5f, 9f );

                switch ( _sessionDropdown.value )
                {
                    case 0:
                        _playerView.SetBreathingIterations ( 12 );
                        break;
                    case 1:
                        _playerView.SetBreathingIterations ( 18 );
                        break;
                    case 2:
                        _playerView.SetBreathingIterations ( 38 );
                        break;
                }
                break;
            case 2:
                _playerView.SetInAndOut ( 6f, 10f );

                switch ( _sessionDropdown.value )
                {
                    case 0:
                        _playerView.SetBreathingIterations ( 10 );
                        break;
                    case 1:
                        _playerView.SetBreathingIterations ( 16 );
                        break;
                    case 2:
                        _playerView.SetBreathingIterations ( 34 );
                        break;
                }
                break;
        }

        ViewManager.Show<JustBreathePlayerView> ();
    }
}
