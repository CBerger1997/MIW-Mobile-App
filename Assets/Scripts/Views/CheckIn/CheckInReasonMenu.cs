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

    private HelpScreen _helpScreen;
    private Button _selectedButton;

    private bool hasCheckedIn = false;
    private int reasonVal = 0;

    public override void Initialise ()
    {
        for ( int i = 0; i < _reasonButtons.Count; i++ )
        {
            int copy = i;
            _reasonButtons[ copy ].onClick.AddListener ( delegate { OnClickContextSelected ( copy, _reasonButtons[ copy ] ); } );
        }

        _iDontKnowButton.onClick.AddListener ( OnIDKButtonClicked );

        _saveButton.onClick.AddListener ( delegate { MoveToNextScreen ( 2 ); } );
        _saveButton.interactable = false;

        _helpScreen = this.GetComponent<HelpScreen> ();
        _helpScreen.ConfigureHelpScreen ();
    }

    public override void Show ()
    {
        base.Show ();

        _helpScreen.ToggleOffHelpMenu ();
    }

    private void OnClickContextSelected ( int val, Button button )
    {
        if ( reasonVal == 14 )
        {
            _iDontKnowButton.GetComponentInChildren<TMPro.TMP_Text> ().color = Color.white;
        }

        if ( _selectedButton != null )
        {
            _selectedButton.GetComponentInChildren<TMPro.TMP_Text> ().color = Color.white;
        }

        _selectedButton = button;
        _selectedButton.GetComponentInChildren<TMPro.TMP_Text> ().color = Color.grey;

        reasonVal = val;
        _saveButton.interactable = true;
    }

    private void OnIDKButtonClicked ()
    {
        if ( _selectedButton != null )
        {
            _selectedButton.GetComponentInChildren<TMPro.TMP_Text> ().color = Color.white;
        }

        _iDontKnowButton.GetComponentInChildren<TMPro.TMP_Text> ().color = Color.grey;
        reasonVal = 14;
        _saveButton.interactable = true;
    }

    private void MoveToNextScreen ( int val )
    {
        _checkinManager.reasonVal = reasonVal;
        _checkinManager.hasCheckedIn = true;

        //AppManager.instance._tData._checkInData.Add ( new TableData.CheckInClass (
        //AppManager.instance._uData.currentEmotionValue,
        //AppManager.instance._uData.currentContextValue,
        //DateTime.Now.ToString () ) );

        ViewManager.Show<MainMenuView> ( false );
    }
}
