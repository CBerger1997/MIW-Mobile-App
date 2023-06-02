using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualisationsMenuView : View {

    [SerializeField] private List<Button> _audioButtons;
    [SerializeField] private GameObject _toggleContent;
    [SerializeField] private VisualisationPlayerView _playerView;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    public AudioClip[] _audioFilesMale;
    public AudioClip[] _audioFilesFemale;
    private int _currentOptionSelection;
    private HelpScreen _helpScreen;

    public override void Initialise () {

        _toggleContent.GetComponent<ScrollSwipe> ().OnSelectionChange += OnSelectionChangeHandler;

        for ( int i = 0; i < _audioButtons.Count; i++ ) {
            int copy = i;
            _audioButtons[ copy ].onClick.AddListener ( () => OnAudioButtonClick ( copy ) );
        }

        _audioFilesMale = Resources.LoadAll<AudioClip> ( @"Audio\Male" );
        _audioFilesFemale = Resources.LoadAll<AudioClip> ( @"Audio\Female" );

        _helpScreen = this.GetComponent<HelpScreen> ();
        _helpScreen.ConfigureHelpScreen ();

        _leftButton.onClick.AddListener ( OnLeftButtonClicked );
        _rightButton.onClick.AddListener ( OnRightButtonClicked );
    }

    private void OnAudioButtonClick ( int index ) {
        if ( _currentOptionSelection == 0 ) {
            _playerView.SetAudioClip ( _audioFilesMale[ index ] );
        } else {
            _playerView.SetAudioClip ( _audioFilesFemale[ index ] );
        }

        ViewManager.Show<VisualisationPlayerView> ();
    }

    public override void Show () {
        base.Show ();

        _helpScreen.ToggleOffHelpMenu ();
    }

    private void OnSelectionChangeHandler () {
        int val = _toggleContent.GetComponent<ScrollSwipe> ().selection;

        if ( val != _currentOptionSelection ) {
            _currentOptionSelection = val;
        }
    }

    //TODO: This needs work, need to figure out a way of changing the first and last sibling without messing up the scrollswipe script


    private void OnLeftButtonClicked () {
        int childCount = _toggleContent.transform.childCount;

        _toggleContent.transform.GetChild ( childCount - 1 ).SetAsFirstSibling ();
    }

    private void OnRightButtonClicked () {
        _toggleContent.transform.GetChild ( 0 ).SetAsLastSibling ();
    }
}