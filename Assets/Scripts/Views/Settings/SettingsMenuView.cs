using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuView : View, IDataPersistence
{
    [SerializeField] private Toggle _checkinToggle;
    [SerializeField] private Toggle _affirmationToggle;
    [SerializeField] private Toggle _journalToggle;
    [SerializeField] private Toggle _visualisationToggle;
    [SerializeField] private Toggle _breathingToggle;
    [SerializeField] private Toggle _commitmentsToggle;

    [SerializeField] private Button _logoutUserButton;
    [SerializeField] private Button _changePasswordButton;
    [SerializeField] private GameObject _topBar;

    bool checkinNotificationsSet;
    bool affirmationNotificationsSet;
    bool journalNotificationsSet;
    bool visualisationNotificationsSet;
    bool breathingNotificationsSet;
    bool commitmentsNotificationsSet;

    public override void Initialise ()
    {
        _logoutUserButton.onClick.AddListener ( LogoutUser );
        _changePasswordButton.onClick.AddListener ( () => ViewManager.Show<ChangePasswordView> () );

        _checkinToggle.onValueChanged.AddListener ( delegate { OnToggleValueChange ( _checkinToggle, 1 ); } );
        _affirmationToggle.onValueChanged.AddListener ( delegate { OnToggleValueChange ( _affirmationToggle, 2 ); } );
        _journalToggle.onValueChanged.AddListener ( delegate { OnToggleValueChange ( _journalToggle, 3 ); } );
        _visualisationToggle.onValueChanged.AddListener ( delegate { OnToggleValueChange ( _visualisationToggle, 4 ); } );
        _breathingToggle.onValueChanged.AddListener ( delegate { OnToggleValueChange ( _breathingToggle, 5 ); } );
        _commitmentsToggle.onValueChanged.AddListener ( delegate { OnToggleValueChange ( _commitmentsToggle, 6 ); } );

        _checkinToggle.isOn = checkinNotificationsSet;
        _affirmationToggle.isOn = affirmationNotificationsSet;
        _journalToggle.isOn = journalNotificationsSet;
        _visualisationToggle.isOn = visualisationNotificationsSet;
        _breathingToggle.isOn = breathingNotificationsSet;
        _commitmentsToggle.isOn = commitmentsNotificationsSet;
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

    private void LogoutUser ()
    {
        DataPersistenceManager.Instance.DeleteUser();
        ViewManager.ClearHistory();
        _topBar.SetActive( false );
        ViewManager.Show<LoginMenuView>(false);
    }

    public void LoadData ( UserData data )
    {
        checkinNotificationsSet = data.checkinNotificationsSet;
        affirmationNotificationsSet = data.affirmationNotificationsSet;
        journalNotificationsSet = data.journalNotificationsSet;
        visualisationNotificationsSet = data.visualisationNotificationsSet;
        breathingNotificationsSet = data.breathingNotificationsSet;
        commitmentsNotificationsSet = data.commitmentsNotificationsSet;
    }

    public void SaveData ( ref UserData data )
    {
        data.checkinNotificationsSet = checkinNotificationsSet;
        data.affirmationNotificationsSet = affirmationNotificationsSet;
        data.journalNotificationsSet = journalNotificationsSet;
        data.visualisationNotificationsSet = visualisationNotificationsSet;
        data.breathingNotificationsSet = breathingNotificationsSet;
        data.commitmentsNotificationsSet = commitmentsNotificationsSet;
    }
}