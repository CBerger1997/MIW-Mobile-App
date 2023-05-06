using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AffirmationMenuView : View, IDataPersistence
{
    [SerializeField] private Button _saveButton;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private GameObject _toggleScreen;
    [SerializeField] private GameObject _listContentParent;
    [SerializeField] private GameObject _listContent;
    [SerializeField] private GameObject _personalContent;
    [SerializeField] private TMP_InputField _personalInputText;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private GameObject _toggleContent;

    private int _savedOptionSelection;
    private int _savedListSelection;
    private string _savedPersonalSelection;

    private int _currentOptionSelection;
    public int _currentListSelection;
    private GameObject _listButton;
    private HelpScreen _helpScreen;

    public override void Initialise ()
    {
        _saveButton.onClick.AddListener ( ViewManager.ShowLast );

        _personalInputText.onValueChanged.AddListener ( delegate { OnInputFieldChanged (); } );

        _toggleContent.GetComponent<ScrollSwipe> ().OnSelectionChange += OnSelectionChangeHandler;

        _saveButton.GetComponent<Button> ().interactable = false;

        _currentListSelection = -1;

        SetAffirmationInfoTextAndContent ();
        SetupAffirmationList ();

        _toggleContent.GetComponent<ScrollSwipe> ().PresetPosition ( _savedOptionSelection );
        _personalInputText.text = _savedPersonalSelection;

        _helpScreen = this.GetComponent<HelpScreen> ();
        _helpScreen.ConfigureHelpScreen ();
    }

    public override void Show ()
    {
        base.Show ();

        _helpScreen.ToggleOffHelpMenu ();
        _saveButton.gameObject.SetActive ( true );
    }

    private void OnSelectionChangeHandler ()
    {
        int val = _toggleContent.GetComponent<ScrollSwipe> ().selection;

        if ( val != _currentOptionSelection )
        {
            _currentOptionSelection = val;
            SetAffirmationInfoTextAndContent ();
        }
    }

    private void SetAffirmationInfoTextAndContent ()
    {
        switch ( _currentOptionSelection )
        {
            case 0:
                _infoText.text = "Provides you with a different affirmation every day";
                _listContentParent.SetActive ( false );
                _personalContent.SetActive ( false );

                if ( _currentOptionSelection != _savedOptionSelection )
                {
                    _saveButton.GetComponent<Button> ().interactable = true;
                }
                else
                {
                    _saveButton.GetComponent<Button> ().interactable = false;
                }
                break;
            case 1:
                _infoText.text = "Choose your affirmation from the list below";
                _listContentParent.SetActive ( true );
                _personalContent.SetActive ( false );

                if ( _currentListSelection < 0 )
                {
                    _saveButton.GetComponent<Button> ().interactable = false;
                }

                break;
            case 2:
                _infoText.text = "Create your own affirmation starting from \"You are\"";
                _listContentParent.SetActive ( false );
                _personalContent.SetActive ( true );

                if ( _personalInputText.text != "" && _personalInputText.text != _savedPersonalSelection )
                {
                    _saveButton.GetComponent<Button> ().interactable = true;
                }
                else
                {
                    _saveButton.GetComponent<Button> ().interactable = false;
                }

                break;
        }
    }

    private void SetupAffirmationList ()
    {
        for ( int i = 0; i < AppManager.instance._aManager.affirmationSelection.Count - 1; i++ )
        {
            GameObject affirmationButton = Instantiate ( _buttonPrefab, _listContent.gameObject.transform );
            affirmationButton.name = i.ToString ();
            affirmationButton.GetComponentInChildren<TMP_Text> ().text = AppManager.instance._aManager.affirmationSelection[ i ];
            affirmationButton.GetComponent<Button> ().onClick.AddListener ( delegate
            { OnListAffirmationClicked ( int.Parse ( affirmationButton.name ), affirmationButton ); } );

            if ( _savedListSelection >= 0 )
            {
                if ( i == _savedListSelection )
                {
                    _currentListSelection = _savedListSelection;
                    _listButton = affirmationButton;
                    _listButton.GetComponentInChildren<TMP_Text> ().color = Color.grey;
                }
            }
        }
    }

    private void OnListAffirmationClicked ( int index, GameObject button )
    {
        if ( _listButton != null )
        {
            _listButton.GetComponentInChildren<TMP_Text> ().color = Color.white;
        }

        _currentListSelection = index;
        _listButton = button;
        _listButton.GetComponentInChildren<TMP_Text> ().color = Color.grey;

        if ( _currentListSelection != _savedListSelection )
        {
            _saveButton.GetComponent<Button> ().interactable = true;
        }
        else
        {
            _saveButton.GetComponent<Button> ().interactable = false;
        }
    }

    private void OnInputFieldChanged ()
    {
        if ( _personalInputText.text != "" && _personalInputText.text != _savedPersonalSelection )
        {
            _saveButton.GetComponent<Button> ().interactable = true;
        }
        else
        {
            _saveButton.GetComponent<Button> ().interactable = false;
        }
    }

    public void LoadData ( UserData data )
    {
        _savedOptionSelection = data.AffSelection;
        _savedListSelection = data.AffListSelection;
        _savedPersonalSelection = data.AffPersonalSelection;
    }

    public void SaveData ( ref UserData data )
    {
        data.AffSelection = _currentOptionSelection;
        data.AffPersonalSelection = "";
        data.AffListSelection = -1;

        switch ( _currentOptionSelection )
        {
            case 1:
                data.AffListSelection = _currentListSelection;
                break;
            case 2:
                data.AffPersonalSelection = _personalInputText.text;
                break;
        }
    }
}