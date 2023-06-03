using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisualisationsMenuView : View {

    [SerializeField] private List<Button> _audioButtons;
    [SerializeField] private GameObject _toggleContent;
    [SerializeField] private VisualisationPlayerView _playerView;

    public AudioClip[] _audioFilesMale;
    public AudioClip[] _audioFilesFemale;
    public int _currentOptionSelection;
    private HelpScreen _helpScreen;

    public override void Initialise () {

        _toggleContent.GetComponent<ScrollSwipe> ().OnSelectionChange += OnSelectionChangeHandler;

        _toggleContent.GetComponent<ScrollSwipe> ().PresetPosition ( 2 );

        _toggleContent.GetComponent<ScrollSwipe>().isInfiniteScroll = true;

        for ( int i = 0; i < _audioButtons.Count; i++ ) {
            int copy = i;
            _audioButtons[ copy ].onClick.AddListener ( () => OnAudioButtonClick ( copy ) );
        }

        _audioFilesMale = Resources.LoadAll<AudioClip> ( @"Audio\Male" );
        _audioFilesFemale = Resources.LoadAll<AudioClip> ( @"Audio\Female" );

        _helpScreen = this.GetComponent<HelpScreen> ();
        _helpScreen.ConfigureHelpScreen ();
    }

    private void OnAudioButtonClick ( int index ) {
        if ( _currentOptionSelection == 0 ) {
            _playerView.SetAudioClip ( _audioFilesMale[ index ] );
        } else {
            _playerView.SetAudioClip ( _audioFilesFemale[ index ] );
        }

        _playerView.SetTitle ( _audioButtons[ index ].GetComponentInChildren<TMP_Text>().text );

        ViewManager.Show<VisualisationPlayerView> ();
    }

    public override void Show () {
        base.Show ();

        _helpScreen.ToggleOffHelpMenu ();
    }

    private void OnSelectionChangeHandler () {
        int val = _toggleContent.GetComponent<ScrollSwipe> ().selection % 2;

        if ( val != _currentOptionSelection ) {
            _currentOptionSelection = val;
        }
    }
}