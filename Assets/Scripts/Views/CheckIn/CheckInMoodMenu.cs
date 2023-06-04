using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CheckInMoodMenu : View
{
    [SerializeField] private List<Button> _emotionButtons = new List<Button> ();
    [SerializeField] private Button _iDontKnowButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _journalButton;
    [SerializeField] private Button _checkInAnalysisButton;
    [SerializeField] private ScrollRect _emotionsRect;
    [SerializeField] private GameObject _toggleContent;
    [SerializeField] private CheckinManager _checkinManager;
    [SerializeField] private GameObject _historyUI;

    private Button _selectedButton;
    private int _currentOptionSelection;
    private HelpScreen _helpScreen;

    private int emotionVal;

    public override void Initialise ()
    {
        for ( int i = 0; i < _emotionButtons.Count; i++ )
        {
            int copy = i;
            _emotionButtons[ copy ].onClick.AddListener ( () => OnClickEmotionSelected ( copy, _emotionButtons[ copy ] ) );
        }

        _iDontKnowButton.onClick.AddListener ( OnIDKButtonClicked );

        _continueButton.onClick.AddListener ( OnContinueButtonClicked );
        _continueButton.interactable = false;

        _journalButton.onClick.AddListener ( () => ViewManager.Show<JournalMenuView> () );
        _checkInAnalysisButton.onClick.AddListener ( () => ViewManager.Show<CheckInAnalysisMenuView> () );

        _toggleContent.GetComponent<ScrollSwipe> ().OnSelectionChange += OnSelectionChangeHandler;

        _emotionsRect.horizontalNormalizedPosition = 0.5f;

        _toggleContent.GetComponent<ScrollSwipe> ().PresetPosition ( 2 );

        _helpScreen = this.GetComponent<HelpScreen> ();
        _helpScreen.ConfigureHelpScreen ();

        _historyUI.gameObject.SetActive ( false );
    }

    public override void Show ()
    {
        base.Show ();

        _helpScreen.ToggleOffHelpMenu ();
        _iDontKnowButton.GetComponentInChildren<TMPro.TMP_Text> ().color = Color.white;

        foreach ( Button emotion in _emotionButtons )
        {
            emotion.GetComponent<Image> ().color = Color.white;
        }

        if ( _checkinManager.isHistoryAvailable )
        {
            _historyUI.gameObject.SetActive ( true );
        }
    }

    private void OnClickEmotionSelected ( int val, Button button )
    {
        if ( emotionVal == 6 )
        {
            _iDontKnowButton.GetComponentInChildren<TMPro.TMP_Text> ().color = Color.white;
        }

        emotionVal = val;
        _continueButton.interactable = true;

        if ( _selectedButton != null )
        {
            _selectedButton.GetComponent<Image> ().color = Color.white;
        }

        _selectedButton = button;
        _selectedButton.GetComponent<Image> ().color = new Color32 ( 139, 210, 235, 255 );
    }

    private void OnIDKButtonClicked ()
    {
        if ( _selectedButton != null )
        {
            _selectedButton.GetComponent<Image> ().color = Color.white;
        }

        _iDontKnowButton.GetComponentInChildren<TMPro.TMP_Text> ().color = Color.grey;
        emotionVal = 6;
        _continueButton.interactable = true;
    }

    private void OnContinueButtonClicked ()
    {
        _checkinManager.curEmotionVal = emotionVal;
        ViewManager.Show<CheckInReasonMenu> ();
    }

    private void OnSelectionChangeHandler ()
    {
        int val = _toggleContent.GetComponent<ScrollSwipe> ().selection;

        if ( val != _currentOptionSelection )
        {
            _currentOptionSelection = val;
        }
    }
}
