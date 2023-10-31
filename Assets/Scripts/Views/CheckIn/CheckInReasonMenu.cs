using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInReasonMenu : View
{
    [SerializeField] private List<Button> _reasonButtons = new List<Button> ();
    [SerializeField] private Button _iDontKnowButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private CheckinManager _checkinManager;
    [SerializeField] private DatabaseHandler _databaseHandler;

    private HelpScreen _helpScreen;
    private List<int> reasonVals = new List<int> ();

    public override void Initialise ()
    {
        for ( int i = 0; i < _reasonButtons.Count; i++ )
        {
            int copy = i;
            _reasonButtons[ copy ].onClick.AddListener ( delegate
            { OnClickContextSelected ( copy, _reasonButtons[ copy ] ); } );
        }

        _iDontKnowButton.onClick.AddListener ( OnIDKButtonClicked );
        _saveButton.onClick.AddListener ( MoveToNextScreen );
        _saveButton.interactable = false;

        _helpScreen = this.GetComponent<HelpScreen> ();
        _helpScreen.ConfigureHelpScreen ();
    }

    public override void Show ()
    {
        base.Show ();

        _helpScreen.ToggleOffHelpMenu ();
        _iDontKnowButton.GetComponentInChildren<TMPro.TMP_Text> ().color = Color.white;

        foreach ( Button reasonButton in _reasonButtons )
        {
            reasonButton.GetComponentInChildren<TMPro.TMP_Text> ().color = Color.white;
        }

    }

    private void OnClickContextSelected ( int val, Button button )
    {
        //Checks there is a current reason value in the list and if it is the IDK button
        if ( reasonVals.Count > 0 && reasonVals[ 0 ] == 14 )
        {
            //Set the IDK button to white and clear the reason values list
            _iDontKnowButton.GetComponentInChildren<TMPro.TMP_Text> ().color = Color.white;
            reasonVals.Clear ();
        }

        //Checks there is a current reason value in the list and if it is the same as the value just pressed
        if ( reasonVals.Count > 0 && reasonVals.Contains ( val ) )
        {
            //Set the button to white and remove it from the reason values list
            button.GetComponentInChildren<TMPro.TMP_Text> ().color = Color.white;
            reasonVals.Remove ( val );

            //Check if the reason values list is now empty
            if ( reasonVals.Count == 0 )
            {
                //Disable the save button
                _saveButton.interactable = false;
            }
        }
        else
        {
            //Set the current selected button to
            //and add it to the reason values list, also enable the save button
            button.GetComponentInChildren<TMPro.TMP_Text> ().color = Color.grey;
            reasonVals.Add ( val );
            _saveButton.interactable = true;
        }
    }

    private void OnIDKButtonClicked ()
    {
        foreach ( Button reasonButton in _reasonButtons )
        {
            reasonButton.GetComponentInChildren<TMPro.TMP_Text> ().color = Color.white;
        }

        _iDontKnowButton.GetComponentInChildren<TMPro.TMP_Text> ().color = Color.grey;
        reasonVals.Clear ();
        reasonVals.Add ( 14 );
        _saveButton.interactable = true;
    }

    private void MoveToNextScreen ()
    {
        string reasons = "";

        for ( int i = 0; i < reasonVals.Count; i++ )
        {
            reasons += reasonVals[ i ];

            if ( reasonVals.Count > 0 && i < reasonVals.Count - 1 )
            {
                reasons += ":";
            }
        }

        Debug.Log ( "Check In Coroutine" );

        _checkinManager.emotionVals.Add ( _checkinManager.curEmotionVal );
        _checkinManager.reasonVals.Add ( reasons );
        _checkinManager.hasCheckedIn = true;

        StartCoroutine ( _databaseHandler.CheckInUser ( _checkinManager.curEmotionVal.ToString (), reasons ) );
    }
}
