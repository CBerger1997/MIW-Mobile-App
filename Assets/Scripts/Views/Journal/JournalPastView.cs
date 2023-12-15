using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JournalPastView : View, IDataPersistence
{
    public List<string> _pastEntries = new List<string> ();
    public List<GameObject> _pastEntryObjects = new List<GameObject> ();

    [SerializeField] private GameObject _pastEntryPrefab;
    [SerializeField] private GameObject _pastEntryParent;
    [SerializeField] private GameObject _lineSeparator;

    public override void Initialise ()
    {

    }

    public override void Show ()
    {
        base.Show ();

        StartCoroutine ( DatabaseHandler.GetUserJournalData () );
    }

    public void SetPastEntries ( List<JournalData> journalHistory )
    {
        _pastEntries.Clear ();

        foreach ( JournalData journal in journalHistory )
        {
            _pastEntries.Add ( journal.journal_date + "," + journal.journal_time + "," + journal.journal_entry );
        }
    }

    public void PopulatePastEntries ()
    {
        string previousDate = "";

        foreach ( string entry in _pastEntries )
        {
            if ( _pastEntryParent.transform.childCount > 0 )
            {
                Instantiate ( _lineSeparator, _pastEntryParent.transform );
            }

            string[] splitString = entry.Split ( ',' );

            GameObject newPastEntry = Instantiate ( _pastEntryPrefab, _pastEntryParent.transform );

            TMP_Text entryTexts = newPastEntry.GetComponentInChildren<TMP_Text> ();

            if ( previousDate == splitString[ 0 ] )
            {
                entryTexts.text = "";
            }
            else
            {
                entryTexts.text = "<size=100%><u>" + splitString[ 0 ] + "</u>\n\n";
            }

            previousDate = splitString[ 0 ];

            entryTexts.text += "<size=90%>" + splitString[ 1 ] + "\n\n";

            entryTexts.text += "<size=80%>";
            for ( int i = 2; i < splitString.Length; i++ )
            {
                entryTexts.text += splitString[ i ];
                if ( i < splitString.Length - 1 )
                {
                    entryTexts.text += ",";
                }
            }

            _pastEntryObjects.Add ( newPastEntry );
        }
    }

    public override void Hide ()
    {
        base.Hide ();

        foreach ( Transform child in _pastEntryParent.transform )
        {
            Destroy ( child.gameObject );
        }
    }

    public void LoadData ( UserData data )
    {

    }

    public void SaveData ( ref UserData data )
    {

    }
}
