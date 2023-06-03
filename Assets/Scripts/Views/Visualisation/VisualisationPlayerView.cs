using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VisualisationPlayerView : View
{

    [SerializeField] private AudioSource _currentAudioSource;
    [SerializeField] private Button _volumeButton;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Slider _videoSlider;
    [SerializeField] private Button _playButton;
    [SerializeField] private Image _playButtonImage;
    [SerializeField] private Sprite _pauseImage;
    [SerializeField] private Sprite _playImage;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _curVidTimeTxt;
    [SerializeField] private TMP_Text _maxVidTimeTxt;

    private bool _isUpdating;

    public override void Initialise ()
    {
        _volumeButton.onClick.AddListener ( OnVolumeButtonClick );
        _volumeSlider.onValueChanged.AddListener ( delegate { OnVolumeChanged (); } );
        _videoSlider.onValueChanged.AddListener ( delegate { OnVideoTimeChanged (); } );
        _playButton.onClick.AddListener ( OnPlayButtonClicked );
        _volumeSlider.gameObject.SetActive ( false );
    }

    private void Update ()
    {
        _isUpdating = true;

        _videoSlider.value = _currentAudioSource.time;

        _curVidTimeTxt.text = TimeToString ( TimeSpan.FromSeconds ( _currentAudioSource.time ).Seconds, TimeSpan.FromSeconds ( _currentAudioSource.time ).Minutes );

        _isUpdating = false;
    }

    public override void Show ()
    {
        base.Show ();

        _currentAudioSource.time = 0;
        _currentAudioSource.Play ();

        _playButtonImage.sprite = _pauseImage;

        _videoSlider.value = 0;
        _videoSlider.maxValue = _currentAudioSource.clip.length;

        _maxVidTimeTxt.text = TimeToString ( TimeSpan.FromSeconds ( _currentAudioSource.clip.length ).Seconds, TimeSpan.FromSeconds ( _currentAudioSource.clip.length ).Minutes );
        _curVidTimeTxt.text = "00:00";
    }

    public override void Hide ()
    {
        base.Hide ();
    }

    private void OnVolumeButtonClick ()
    {
        _volumeSlider.gameObject.SetActive ( _volumeSlider.gameObject.activeSelf ? false : true );
    }

    private void OnVolumeChanged ()
    {
        _currentAudioSource.volume = _volumeSlider.value;
    }

    private void OnVideoTimeChanged ()
    {
        if(!_isUpdating)
        {
            _currentAudioSource.time = _videoSlider.value;
        }
    }

    private void OnPlayButtonClicked ()
    {
        if ( _currentAudioSource.isPlaying )
        {
            _currentAudioSource.Pause ();
            _playButtonImage.sprite = _playImage;
        }
        else
        {
            _currentAudioSource.Play ();
            _playButtonImage.sprite = _pauseImage;
        }
    }

    private string TimeToString ( float seconds, float minutes )
    {
        string minutesText = minutes.ToString ();
        string secondsText = seconds.ToString ();

        if ( minutes < 10 )
        {
            minutesText = "0" + minutes;
        }

        if ( seconds < 10 )
        {
            secondsText = "0" + seconds;
        }

        return minutesText + ":" + secondsText;
    }

    public void SetAudioClip ( AudioClip clip )
    {
        _currentAudioSource.clip = clip;
    }

    public void SetTitle (string text)
    {
        _titleText.text = text;
    }
}