using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualisationsMenuView : View {
    [SerializeField] private Button _volumeButton;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private List<Button> _audioButtons;
    [SerializeField] private AudioSource _currentAudioSource;
    [SerializeField] private GameObject _toggleContent;
    [SerializeField] private AudioSource _audioPlayer;
    //[SerializeField] private AutoSpaceOnResolution _autoSpaceOnResolution;

    private AudioClip[] _audioFilesMale;
    private AudioClip[] _audioFilesFemale;
    private int _currentOptionSelection;

    public override void Initialise() {
        _volumeButton.onClick.AddListener(() => OnVolumeButtonClick());
        _volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChanged(); });

        _toggleContent.GetComponent<ScrollSwipe>().OnSelectionChange += OnSelectionChangeHandler;

        for (int i = 0; i < _audioButtons.Count; i++) {
            int copy = i;
            _audioButtons[copy].onClick.AddListener(() => OnAudioButtonClick(copy));
        }

        _volumeSlider.gameObject.SetActive(false);

        _audioFilesMale = Resources.LoadAll<AudioClip>(@"Audio\Male");
        _audioFilesFemale = Resources.LoadAll<AudioClip>(@"Audio\Female");

        //_autoSpaceOnResolution.PerformAutoSpace();
    }

    private void OnAudioButtonClick(int index) {
        if (_audioPlayer.clip != null && _audioPlayer.isPlaying) {
            _currentAudioSource.Stop();
        }

        AudioClip clip;

        if(_currentOptionSelection == 0) {
            clip = _audioFilesMale[index];
        } else {
            clip = _audioFilesFemale[index];
        }

        _audioPlayer.clip = clip;
        _audioPlayer.Play();
    }

    private void OnVolumeButtonClick() {
        _volumeSlider.gameObject.SetActive(_volumeSlider.gameObject.activeSelf ? false : true);
    }

    private void OnVolumeChanged() {
        _currentAudioSource.volume = _volumeSlider.value;
    }

    private void OnSelectionChangeHandler() {
        int val = _toggleContent.GetComponent<ScrollSwipe>().selection;

        if (val != _currentOptionSelection) {
            _currentOptionSelection = val;
        }
    }
}
