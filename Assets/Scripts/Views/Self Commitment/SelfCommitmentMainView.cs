using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelfCommitmentMainView : View, IDataPersistence
{
    [SerializeField] private TMP_Text _commitmentText;
    [SerializeField] private TMP_Text _isDoneText;
    [SerializeField] private Toggle _isDoneToggle;
    [SerializeField] private Button _editButton;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private Button _deleteUIButton;
    [SerializeField] private Button _cancelUIButton;
    [SerializeField] private GameObject _deleteUI;
    [SerializeField] private SelfCommitmentEditView _editView;
    [SerializeField] private RectTransform _contentsRect;

    private HelpScreen _helpScreen;
    private bool _isDelete;

    public override void Initialise ()
    {
        _helpScreen = this.GetComponent<HelpScreen> ();
        _helpScreen.ConfigureHelpScreen ();
        _deleteUI.SetActive ( false );

        _editButton.onClick.AddListener ( OnEditCicked );
        _deleteButton.onClick.AddListener ( OnDeleteClicked );
        _deleteUIButton.onClick.AddListener ( OnDeleteClicked );
        _cancelUIButton.onClick.AddListener ( OnCancelClicked );

        _isDoneToggle.onValueChanged.AddListener ( delegate { OnDoneToggleChanged (); } );

        _isDelete = false;
    }

    public override void Show ()
    {
        base.Show ();

        _helpScreen.ToggleOffHelpMenu ();

        _deleteUI.SetActive ( false );

        LayoutRebuilder.ForceRebuildLayoutImmediate ( _contentsRect );
    }

    public override void Hide ()
    {
        base.Hide ();
    }

    private void OnEditCicked ()
    {
        ViewManager.Show<SelfCommitmentEditView> ();
    }

    private void OnDoneToggleChanged ()
    {
        DataPersistenceManager.Instance.SaveUser ();
        DataPersistenceManager.Instance.LoadUser ();
    }

    private void OnDeleteClicked ()
    {
        if ( !_deleteUI.activeSelf )
        {
            _deleteUI.SetActive ( true );
        }
        else
        {
            _isDelete = true;
            _isDoneToggle.isOn = false;

            _editView.DeleteCommitmentValues ();

            DataPersistenceManager.Instance.SaveUser ();
            DataPersistenceManager.Instance.LoadUser ();
        }
    }

    private void OnCancelClicked ()
    {
        _deleteUI.SetActive ( false );
    }

    public void LoadData ( UserData data )
    {
        string oftenText = ConvertDropdownValueToString ( data.commitmentOftenVal );

        string reminderText;

        if ( data.isReminder )
        {
            string reminderOftenText = ConvertDropdownValueToString ( data.reminderOftenVal );

            string reminderTime = "";

            if ( data.reminderHourVal < 10 )
            {
                reminderTime = "0";
            }

            reminderTime += data.reminderHourVal.ToString () + ":";

            if ( data.reminderMinuteVal < 10 )
            {
                reminderTime += "0";
            }

            reminderTime += data.reminderMinuteVal.ToString ();

            reminderText = reminderOftenText + " reminders at " + reminderTime;
        }
        else
        {
            reminderText = "No reminders set";
        }

        _commitmentText.text =
            data.commitmentString + "\n\n"
            + "To do: " + oftenText + "\n\n"
            + reminderText;


        switch ( data.commitmentOftenVal )
        {
            case 0:
                _isDoneText.text = "I have done it today";
                break;
            case 1:
                _isDoneText.text = "I have done it this week";
                break;
            case 2:
                _isDoneText.text = "I have done it this month";
                break;
        }
    }

    public void SaveData ( ref UserData data )
    {
        data.isCommitmentDone = _isDoneToggle.isOn;

        if ( _isDelete )
        {
            data.commitmentString = "";
            data.commitmentOftenVal = 0;
            data.isReminder = false;
            data.reminderHourVal = 0;
            data.reminderMinuteVal = 0;
            data.isCommitmentDone = false;
            data.commitmentExists = false;

            _isDelete = false;

            ViewManager.Show<SelfCommitmentEditView> ( false );
        }
    }

    private string ConvertDropdownValueToString ( int value )
    {
        string text = "";

        switch ( value )
        {
            case 0:
                text = "Daily";
                break;
            case 1:
                text = "Weekly";
                break;
            case 2:
                text = "Monthly";
                break;
        }

        return text;
    }
}
