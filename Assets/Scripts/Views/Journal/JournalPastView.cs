using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalPastView : View, IDataPersistence {
    public List<string> _pastEntries = new List<string> ();
    public List<GameObject> _pastEntryObjects = new List<GameObject> ();

    [SerializeField] private GameObject _pastEntryPrefab;
    [SerializeField] private GameObject _pastEntryParent;

    public override void Initialise () {
        string previousDate = "";

        foreach ( string entry in _pastEntries ) {
            string[] splitString = entry.Split ( ',' );

            GameObject newPastEntry = Instantiate ( _pastEntryPrefab, _pastEntryParent.transform );

            TMP_Text[] entryTexts = newPastEntry.GetComponentsInChildren<TMP_Text> ();

            if ( previousDate == splitString[ 0 ] ) {
                Destroy ( entryTexts[ 0 ] );
            } else {
                entryTexts[ 0 ].text = splitString[ 0 ];
            }

            previousDate = splitString[ 0 ];

            entryTexts[ 1 ].text = splitString[ 1 ];
            entryTexts[ 2 ].text = "";

            for ( int i = 2; i < splitString.Length; i++ ) {
                entryTexts[ 2 ].text += splitString[ i ];
                if ( i < splitString.Length - 1 ) {
                    entryTexts[ 2 ].text += ",";
                }
            }

            _pastEntryObjects.Add ( newPastEntry );
        }
    }

    public override void Show () {
        base.Show ();
    }

    public void LoadData ( UserData data ) {
        _pastEntries = data.dataEntries;
    }

    public void SaveData ( ref UserData data ) {

    }
}
