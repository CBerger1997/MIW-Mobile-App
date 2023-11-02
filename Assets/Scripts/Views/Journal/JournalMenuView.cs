using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class JournalMenuView : View, IDataPersistence
{

    [SerializeField] private TMP_Text _dateText;
    [SerializeField] private TMP_Text _limitText;
    [SerializeField] private TMP_InputField _journalEntryInput;
    [SerializeField] private Button _saveEntryButton;
    [SerializeField] private Button _pastEntriesButton;

    private List<string> _journalEntries = new List<string> ();
    private HelpScreen _helpScreen;

    public override void Initialise ()
    {
        _dateText.text = "Entry: " + DateTime.Now.ToString ( "ddd" ) + ", " + DateTime.Now.ToString ( "d" );

        _saveEntryButton.onClick.AddListener ( OnSaveEntryClicked );
        _pastEntriesButton.onClick.AddListener ( delegate
        { ViewManager.Show<JournalPastView> (); } );
        _journalEntryInput.onValueChanged.AddListener ( delegate
        { OnInputFieldChanged (); } );

        _helpScreen = this.GetComponent<HelpScreen> ();
        _helpScreen.ConfigureHelpScreen ();
    }

    public override void Show ()
    {
        base.Show ();

        _journalEntryInput.text = "";
        _helpScreen.ToggleOffHelpMenu ();
        _saveEntryButton.GetComponent<Button> ().interactable = false;
    }

    private void OnSaveEntryClicked ()
    {
        _journalEntries.Add ( DateTime.Now.ToString ( "yyyy/MM/dd" ) + "," + DateTime.UtcNow.ToString ( "HH:mm" ) + "," + _journalEntryInput.text );

        StartCoroutine ( DatabaseHandler.JournalUser ( _journalEntryInput.text ) );
    }

    private void OnInputFieldChanged ()
    {
        if ( _journalEntryInput.text != "" )
        {
            _saveEntryButton.GetComponent<Button> ().interactable = true;
        }
        else
        {
            _saveEntryButton.GetComponent<Button> ().interactable = false;
        }

        _limitText.text = "Limit : " + _journalEntryInput.text.Length + " / 500";
    }

    public void LoadData ( UserData data )
    {

    }

    public void SaveData ( ref UserData data )
    {
        foreach ( string entry in _journalEntries )
        {
            data.dataEntries.Add ( entry );
        }

        _journalEntries.Clear ();
    }
}
