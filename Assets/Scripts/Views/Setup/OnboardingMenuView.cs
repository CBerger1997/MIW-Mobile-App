using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnboardingMenuView : View, IDataPersistence
{
    bool checkinNotificationsSet;
    bool affirmationNotificationsSet;
    bool journalNotificationsSet;
    bool visualisationNotificationsSet;
    bool breathingNotificationsSet;
    bool commitmentsNotificationsSet;

    [SerializeField] private Toggle _checkinToggle;
    [SerializeField] private Toggle _affirmationToggle;
    [SerializeField] private Toggle _journalToggle;
    [SerializeField] private Toggle _visualisationToggle;
    [SerializeField] private Toggle _breathingToggle;
    [SerializeField] private Toggle _commitmentsToggle;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _affirmationButton;
    [SerializeField] private GameObject _permanentMenu;

    private bool onBoardingDone = false;

    public override void Initialise ()
    {
        _continueButton.onClick.AddListener ( ContinueButtonOnClick );
        _affirmationButton.onClick.AddListener ( () => ViewManager.Show<AffirmationMenuView> () );

        _checkinToggle.onValueChanged.AddListener ( delegate { OnToggleValueChange ( _checkinToggle, 1 ); } );
        _affirmationToggle.onValueChanged.AddListener ( delegate { OnToggleValueChange ( _affirmationToggle, 2 ); } );
        _journalToggle.onValueChanged.AddListener ( delegate { OnToggleValueChange ( _journalToggle, 3 ); } );
        _visualisationToggle.onValueChanged.AddListener ( delegate { OnToggleValueChange ( _visualisationToggle, 4 ); } );
        _breathingToggle.onValueChanged.AddListener ( delegate { OnToggleValueChange ( _breathingToggle, 5 ); } );
        _commitmentsToggle.onValueChanged.AddListener ( delegate { OnToggleValueChange ( _commitmentsToggle, 6 ); } );

        _checkinToggle.isOn = true;
        _affirmationToggle.isOn = true;
        _journalToggle.isOn = true;
        _visualisationToggle.isOn = true;
        _breathingToggle.isOn = true;
        _commitmentsToggle.isOn = true;

        checkinNotificationsSet = true;
        affirmationNotificationsSet = true;
        journalNotificationsSet = true;
        visualisationNotificationsSet = true;
        breathingNotificationsSet = true;
        commitmentsNotificationsSet = true;
    }

    private void OnToggleValueChange ( Toggle toggle, int val )
    {
        switch ( val )
        {
            case 1:
                checkinNotificationsSet = toggle.isOn;
                break;
            case 2:
                affirmationNotificationsSet = toggle.isOn;
                break;
            case 3:
                journalNotificationsSet = toggle.isOn;
                break;
            case 4:
                visualisationNotificationsSet = toggle.isOn;
                break;
            case 5:
                breathingNotificationsSet = toggle.isOn;
                break;
            case 6:
                commitmentsNotificationsSet = toggle.isOn;
                break;
        }
    }

    private void ContinueButtonOnClick ()
    {
        onBoardingDone = true;

        _permanentMenu.SetActive ( true );
        ViewManager.Show<CheckInMoodMenu> ( false );
    }

    public void LoadData ( UserData data )
    {
        onBoardingDone = data.hasOnboarded;
    }

    public void SaveData ( ref UserData data )
    {
        data.hasOnboarded = onBoardingDone;

        data.checkinNotificationsSet = checkinNotificationsSet;
        data.affirmationNotificationsSet = affirmationNotificationsSet;
        data.journalNotificationsSet = journalNotificationsSet;
        data.visualisationNotificationsSet = visualisationNotificationsSet;
        data.breathingNotificationsSet = breathingNotificationsSet;
        data.commitmentsNotificationsSet = commitmentsNotificationsSet;
    }
}
