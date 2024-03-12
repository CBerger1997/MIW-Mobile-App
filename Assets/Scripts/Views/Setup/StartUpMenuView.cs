using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class StartUpMenuView : View, IDataPersistence {
    [SerializeField] private List<GameObject> _firstTimeStartUp;
    [SerializeField] private List<GameObject> _startUpPage;
    [SerializeField] private GameObject _permanentMenu;
    [SerializeField] private GameObject _startUpParent1;
    [SerializeField] private GameObject _startUpParent2;
    [SerializeField] private Button _continueButton1;
    [SerializeField] private Button _continueButton2;

    Animator _animator;

    bool isUserTouchingScreen = false;
    bool hasOnboarded = false;
    bool hasCheckedIn = false;

    private int _savedOptionSelection;
    private int _savedListSelection;
    private string _savedPersonalSelection;

    public override void Initialise () {

        _continueButton1.onClick.AddListener ( delegate { ContinueButtonClicked (); } );
        _continueButton2.onClick.AddListener ( delegate { ContinueButtonClicked (); } );

        SetQuoteText ();

        SetAffirmationText ();

        _startUpParent1.SetActive ( false );
        _startUpParent2.SetActive ( false );

        if ( !hasOnboarded ) {
            _startUpParent1.SetActive ( true );
            _startUpParent2.SetActive ( false );
        } else {
            _startUpParent1.SetActive ( false );
            _startUpParent2.SetActive ( true );
        }
        _animator = GetComponent<Animator> ();

        Canvas.ForceUpdateCanvases ();
    }

    private void Update () {
        if ( ( Input.touchCount > 0 || Input.GetMouseButtonDown ( 0 ) ) && !isUserTouchingScreen ) {
            isUserTouchingScreen = true;

            if ( _animator.GetCurrentAnimatorStateInfo ( 0 ).IsName ( "StartUpAnim" ) ) {
                _animator.Play ( "StaticState" );
            }
        } else if ( Input.touchCount == 0 ) {
            isUserTouchingScreen = false;
        }
    }

    private void SetQuoteText () {
        int randomNum = UnityEngine.Random.Range ( 0, AppManager.instance._qManager.author.Count - 1 );

        string text = AppManager.instance._qManager.quote[ randomNum ] +
            "\n \n" +
            AppManager.instance._qManager.author[ randomNum ];

        _firstTimeStartUp[ 1 ].GetComponent<TMP_Text> ().text = text;
        _startUpPage[ 0 ].GetComponent<TMP_Text> ().text = text;
    }

    private void SetAffirmationText () {
        string text = "";

        switch ( _savedOptionSelection ) {
            case 0:
                int randomNum = UnityEngine.Random.Range ( 0, AppManager.instance._aManager.affirmationRandom.Count - 1 );
                text = AppManager.instance._aManager.affirmationRandom[ randomNum ];
                break;
            case 1:
                text = AppManager.instance._aManager.affirmationSelection[ _savedListSelection ];
                break;
            case 2:
                text = _savedPersonalSelection;
                break;
        }

        _startUpPage[ 1 ].GetComponent<TMP_Text> ().text = text;
    }

    private void ContinueButtonClicked () {
        if ( !hasOnboarded ) {
            ViewManager.Show<OnboardingMenuView> ( false );
        } else if ( !hasCheckedIn ) {
            _permanentMenu.SetActive ( true );
            ViewManager.Show<CheckInMoodMenu> ( false );
        } else {
            _permanentMenu.SetActive ( true );
            ViewManager.Show<MainMenuView> ( false );
        }
    }

    public void LoadData ( UserData data ) {
        hasOnboarded = data.hasOnboarded;

        //hasCheckedIn = data.emotionDates.Contains ( DateTime.Now.ToString ( "yyyy/MM/dd" ) );

        _savedOptionSelection = data.AffSelection;
        _savedListSelection = data.AffListSelection;
        _savedPersonalSelection = data.AffPersonalSelection;
    }

    public void SaveData ( ref UserData data ) {
        //Not required for this view
    }
}