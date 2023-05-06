using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VisualisationPlayerView : View {

    [SerializeField] private AudioSource _currentAudioSource;
    [SerializeField] private Button _volumeButton;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Button _playButton;
    [SerializeField] private Image _playButtonImage;
    [SerializeField] private List<GameObject> _backPanels;
    [SerializeField] private TopBarManager _topBar;
    [SerializeField] private GameObject _backPanel;
    [SerializeField] private Sprite _pauseImage;
    [SerializeField] private Sprite _playImage;

    private bool isPanelsActive = false;
    private bool isUserTouchingScreen = false;
    private float screenShowTimer = 0.0f;

    public override void Initialise () {
        _volumeButton.onClick.AddListener ( OnVolumeButtonClick );
        _volumeSlider.onValueChanged.AddListener ( delegate { OnVolumeChanged (); } );
        _playButton.onClick.AddListener ( OnPlayButtonClicked );
        _volumeSlider.gameObject.SetActive ( false );
    }

    private void Update () {
        if ( ( Input.touchCount > 0 || Input.GetMouseButtonDown ( 0 ) ) && !isUserTouchingScreen ) {
            isUserTouchingScreen = true;
            screenShowTimer = 5.0f;

            if ( !isPanelsActive ) {
                foreach ( GameObject panel in _backPanels ) {
                    panel.SetActive ( true );
                }

                _topBar.gameObject.SetActive ( true );
                isPanelsActive = true;
            } else {
                PointerEventData eventDataCurrentPosition = new PointerEventData ( EventSystem.current );
                eventDataCurrentPosition.position = new Vector2 ( Input.GetTouch ( 0 ).position.x, Input.GetTouch ( 0 ).position.y );
                List<RaycastResult> results = new List<RaycastResult> ();
                EventSystem.current.RaycastAll ( eventDataCurrentPosition, results );

                if ( results.Count > 0 && results[ 0 ].gameObject.name.Equals ( _backPanel.name ) ) {
                    foreach ( GameObject panel in _backPanels ) {
                        panel.SetActive ( false );
                    }

                    _topBar.gameObject.SetActive ( false );
                    screenShowTimer = 0.0f;
                    isPanelsActive = false;
                }
            }
        } else if ( Input.touchCount == 0 ) {
            isUserTouchingScreen = false;
        }

        if ( screenShowTimer > 0.0f ) {
            screenShowTimer -= Time.deltaTime;

            if ( screenShowTimer <= 0.0f ) {
                foreach ( GameObject panel in _backPanels ) {
                    panel.SetActive ( false );
                }

                _topBar.gameObject.SetActive ( false );
            }
        }
    }

    public override void Show () {
        base.Show ();

        _currentAudioSource.Play ();

        _playButtonImage.sprite = _pauseImage;

        foreach ( GameObject panel in _backPanels ) {
            panel.SetActive ( false );
        }

        _topBar.gameObject.SetActive ( false );
        isPanelsActive = false;
        isUserTouchingScreen = false;
        screenShowTimer = 0.0f;
    }

    public override void Hide () {
        base.Hide ();
    }

    private void OnVolumeButtonClick () {
        _volumeSlider.gameObject.SetActive ( _volumeSlider.gameObject.activeSelf ? false : true );
    }

    private void OnVolumeChanged () {
        _currentAudioSource.volume = _volumeSlider.value;
    }

    private void OnPlayButtonClicked () {
        if ( _currentAudioSource.isPlaying ) {
            _currentAudioSource.Pause ();
            _playButtonImage.sprite = _playImage;
        } else {
            _currentAudioSource.Play ();
            _playButtonImage.sprite = _pauseImage;
        }
    }

    public void SetAudioClip ( AudioClip clip ) {
        _currentAudioSource.clip = clip;
    }
}