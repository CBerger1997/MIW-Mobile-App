using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Notifications.Android;
using UnityEngine.Android;

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

    List<Notification> notificationList = new List<Notification> ();

    public override void Initialise ()
    {
        if ( !Permission.HasUserAuthorizedPermission ( "android.permission.POST_NOTIFICATIONS" ) )
        {
            Permission.RequestUserPermission ( "android.permission.POST_NOTIFICATIONS" );
        }

        //Creating new channel to show notifications
        var channel = new AndroidNotificationChannel ()
        {
            Id = "InnerCalm",
            Name = "Inner Calm Channel",
            Importance = Importance.Default,
            Description = "Inner Calm Notification",
        };

        AndroidNotificationCenter.RegisterNotificationChannel ( channel );

        _logoutUserButton.onClick.AddListener ( LogoutUser );
        _changePasswordButton.onClick.AddListener ( () => ViewManager.Show<ChangePasswordView> () );

        _checkinToggle.onValueChanged.AddListener ( delegate
        { OnToggleValueChange ( _checkinToggle, 1 ); } );
        _affirmationToggle.onValueChanged.AddListener ( delegate
        { OnToggleValueChange ( _affirmationToggle, 2 ); } );
        _journalToggle.onValueChanged.AddListener ( delegate
        { OnToggleValueChange ( _journalToggle, 3 ); } );
        _visualisationToggle.onValueChanged.AddListener ( delegate
        { OnToggleValueChange ( _visualisationToggle, 4 ); } );
        _breathingToggle.onValueChanged.AddListener ( delegate
        { OnToggleValueChange ( _breathingToggle, 5 ); } );
        _commitmentsToggle.onValueChanged.AddListener ( delegate
        { OnToggleValueChange ( _commitmentsToggle, 6 ); } );

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

                if ( commitmentsNotificationsSet )
                {
                    ScheduleNotifications ();
                }
                else
                {
                    AndroidNotificationCenter.CancelAllNotifications ();
                }

                break;
        }
    }

    private void LogoutUser ()
    {
        DataPersistenceManager.Instance.DeleteUser ();
        ViewManager.ClearHistory ();
        _topBar.SetActive ( false );
        ViewManager.Show<LoginMenuView> ( false );
    }

    private void ScheduleNotifications ()
    {
        //AndroidNotificationCenter.CancelAllNotifications ();

        //Send notifications for the next 7 days

        ScheduleNotification ( "Inner Calm: Commitment", "Don't forget to check in and follow your commitment today!", 1, System.DateTime.Now.Date.AddDays ( 1 ).AddHours ( 8 ) );
        ScheduleNotification ( "Inner Calm: Commitment", "Don't forget to check in and follow your commitment today!", 2, System.DateTime.Now.Date.AddDays ( 2 ).AddHours ( 8 ) );
        ScheduleNotification ( "Inner Calm: Commitment", "Don't forget to check in and follow your commitment today!", 3, System.DateTime.Now.Date.AddDays ( 3 ).AddHours ( 8 ) );
        ScheduleNotification ( "Inner Calm: Commitment", "Don't forget to check in and follow your commitment today!", 4, System.DateTime.Now.Date.AddDays ( 4 ).AddHours ( 8 ) );
        ScheduleNotification ( "Inner Calm: Commitment", "Don't forget to check in and follow your commitment today!", 5, System.DateTime.Now.Date.AddDays ( 5 ).AddHours ( 8 ) );
        ScheduleNotification ( "Inner Calm: Commitment", "Don't forget to check in and follow your commitment today!", 6, System.DateTime.Now.Date.AddDays ( 6 ).AddHours ( 8 ) );
        ScheduleNotification ( "Inner Calm: Commitment", "Don't forget to check in and follow your commitment today!", 7, System.DateTime.Now.Date.AddDays ( 7 ).AddHours ( 8 ) );
    }

    private void ScheduleNotification ( string notifTitle, string notifText, int id, System.DateTime timeFrame )
    {
        var notification = new AndroidNotification ();

        notification.Title = notifTitle;
        notification.Text = notifText;
        notification.SmallIcon = "icon";
        notification.LargeIcon = "logo";

        //Time notification
        notification.FireTime = timeFrame;

        //Send notification
        AndroidNotificationCenter.SendNotificationWithExplicitID ( notification, "InnerCalm", id );
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