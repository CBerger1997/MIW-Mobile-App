using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelfCommitmentEditView : View, IDataPersistence
{
    [SerializeField] private TMP_InputField _commitmentInputfield;
    [SerializeField] private TMP_Dropdown _commitmentDropdown;
    [SerializeField] private Toggle _reminderToggle;
    [SerializeField] private GameObject _reminderUI;
    [SerializeField] private TMP_Dropdown _reminderDropdown;
    [SerializeField] private TMP_Dropdown _hourDropdown;
    [SerializeField] private TMP_Dropdown _minuteDropdown;
    [SerializeField] private Button _saveButton;

    private HelpScreen _helpScreen;
    private bool _commitmentExists = false;
    private int _hourVal;
    private int _minuteVal;

    public override void Initialise ()
    {
        _helpScreen = this.GetComponent<HelpScreen> ();
        _helpScreen.ConfigureHelpScreen ();
        _reminderUI.SetActive ( false );

        _commitmentInputfield.onValueChanged.AddListener ( delegate { CommitmentInputChanged (); } );
        _reminderToggle.onValueChanged.AddListener ( delegate { ReminderToggleChanged (); } );
        _saveButton.onClick.AddListener ( OnSaveClicked );
        _saveButton.interactable = false;

        List<string> dropdownHours = new List<string> ();
        List<string> dropdownMinutes = new List<string> ();

        for ( int i = 0; i < 24; i++ )
        {
            if ( i < 10 )
            {
                dropdownHours.Add ( "0" + ( i ).ToString () );
            }
            else
            {
                dropdownHours.Add ( ( i ).ToString () );
            }
        }

        for ( int i = 0; i < 60; i++ )
        {
            if ( i < 10 )
            {
                dropdownMinutes.Add ( "0" + ( i ).ToString () );
            }
            else
            {
                dropdownMinutes.Add ( ( i ).ToString () );
            }
        }

        _hourDropdown.AddOptions ( dropdownHours );
        _minuteDropdown.AddOptions ( dropdownMinutes );
    }

    public override void Show ()
    {
        base.Show ();

        _helpScreen.ToggleOffHelpMenu ();

        if ( _commitmentInputfield.text != "" )
        {
            _saveButton.interactable = true;
        }

        if ( _reminderToggle.isOn )
        {
            _reminderUI.SetActive ( true );
            _hourDropdown.value = _hourVal;
            _minuteDropdown.value = _minuteVal;
        }
    }

    private void CommitmentInputChanged ()
    {
        _saveButton.interactable = _commitmentInputfield.text == "" ? false : true;
    }

    private void ReminderToggleChanged ()
    {
        _reminderUI.SetActive ( _reminderToggle.isOn ? true : false );
    }

    public void DeleteCommitmentValues ()
    {
        _commitmentInputfield.text = "";
        _commitmentDropdown.value = 0;
        _reminderToggle.isOn = false;
        _reminderUI.SetActive ( false );
        _reminderDropdown.value = 0;
        _hourDropdown.value = 0;
        _minuteDropdown.value = 0;
        _commitmentExists = false;
    }

    private void OnSaveClicked ()
    {
        _commitmentExists = true;

        DataPersistenceManager.Instance.SaveUser ();
        DataPersistenceManager.Instance.LoadUser ();

        ViewManager.Show<SelfCommitmentMainView> ( false );
    }

    public void LoadData ( UserData data )
    {

        _commitmentExists = data.commitmentExists;

        _commitmentInputfield.text = data.commitmentString;
        _commitmentDropdown.value = data.commitmentOftenVal;
        _reminderToggle.isOn = data.isReminder;

        Debug.Log ( _reminderToggle.isOn );

        if ( _reminderToggle.isOn )
        {
            _reminderDropdown.value = data.reminderOftenVal;
            _hourVal = data.reminderHourVal;
            _minuteVal = data.reminderMinuteVal;
            _reminderUI.SetActive ( true );
        }
    }

    public void SaveData ( ref UserData data )
    {
        data.commitmentExists = _commitmentExists;

        data.commitmentString = _commitmentInputfield.text;
        data.commitmentOftenVal = _commitmentDropdown.value;
        data.isReminder = _reminderToggle.isOn;

        if ( _reminderToggle.isOn )
        {
            data.reminderOftenVal = _reminderDropdown.value;
            data.reminderHourVal = _hourDropdown.value;
            data.reminderMinuteVal = _minuteDropdown.value;
        }
    }
}
