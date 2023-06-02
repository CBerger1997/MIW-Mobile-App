using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class JournalMenuView : View, IDataPersistence {

    [SerializeField] private TMP_Text _dateText;
    [SerializeField] private TMP_InputField _journalEntryInput;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _pastEntriesButton;

    private List<string> _journalEntries = new List<string> ();
    private HelpScreen _helpScreen;

    public override void Initialise () {
        _dateText.text = "Entry: " + DateTime.Now.ToString ( "ddd" ) + ", " + DateTime.Now.ToString ( "d" );

        _continueButton.onClick.AddListener ( OnContinueClicked );
        _pastEntriesButton.onClick.AddListener ( delegate { ViewManager.Show<JournalPastView> (); } );

        _helpScreen = this.GetComponent<HelpScreen> ();
        _helpScreen.ConfigureHelpScreen ();
    }

    public override void Show () {
        base.Show ();

        _journalEntryInput.text = "";
        _helpScreen.ToggleOffHelpMenu ();
    }

    private void OnContinueClicked () {
        _journalEntries.Add ( DateTime.Now.ToString ( "yyyy/MM/dd" ) + "," + DateTime.UtcNow.ToString ( "HH:mm" ) + "," + _journalEntryInput.text );
        ViewManager.ShowLast ();
    }

    public void LoadData ( UserData data ) {

    }

    public void SaveData ( ref UserData data ) {
        foreach ( string entry in _journalEntries ) {
            data.dataEntries.Add ( entry );
        }

        _journalEntries.Clear ();
    }
}
