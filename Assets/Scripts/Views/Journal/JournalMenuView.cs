using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class JournalMenuView : View {

    [SerializeField] private TMP_Text _dateText;
    [SerializeField] private TMP_Text _savedText;
    [SerializeField] private TMP_InputField _journalEntryInput;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _pastEntriesButton;

    private string _journalEntry;
    private HelpScreen _helpScreen;

    public override void Initialise() {
        _dateText.text = "Entry: " + DateTime.Now.ToString("ddd") + ", " + DateTime.Now.ToString("d");
        _savedText.gameObject.SetActive(false);

        _saveButton.onClick.AddListener(OnSavedClicked);
        _continueButton.onClick.AddListener(delegate { ViewManager.ShowLast(); });

        _helpScreen = this.GetComponent<HelpScreen> ();
        _helpScreen.ConfigureHelpScreen ();
    }

    public override void Show() {
        base.Show();

        _savedText.gameObject.SetActive(false);
        _journalEntryInput.text = "";
        _helpScreen.ToggleOffHelpMenu ();
    }

    private void OnSavedClicked() {
        _journalEntry = DateTime.Now.ToString("d") + "," + _journalEntryInput.text;

        //Save the journal entry to user data

        _savedText.gameObject.SetActive(true);
    }
}
