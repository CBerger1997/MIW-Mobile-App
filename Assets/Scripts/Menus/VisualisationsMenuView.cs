using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualisationsMenuView : View {
    [SerializeField] private Button _volumeButton;
    [SerializeField] private Slider _volumeSlider;

    [SerializeField] private List<Button> _audioButtons;

    [SerializeField] private AudioSource _currentAudioSource;

    public override void Initialise() {
        _volumeButton.onClick.AddListener(() => OnVolumeButtonClick());
        _volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChanged(); });


        for (int i = 0; i < _audioButtons.Count; i++) {
            int copy = i;
            _audioButtons[copy].onClick.AddListener(() => OnAudioButtonClick(copy));
        }

        _volumeSlider.gameObject.SetActive(false);
    }

    private void OnAudioButtonClick(int index) {
        if (_currentAudioSource != null && _currentAudioSource.isPlaying) {
            _currentAudioSource.Stop();
        }

        _audioButtons[index].GetComponent<AudioSource>().Play();
        _currentAudioSource = _audioButtons[index].GetComponent<AudioSource>();
    }

    private void OnVolumeButtonClick() {
        _volumeSlider.gameObject.SetActive(_volumeSlider.gameObject.activeSelf ? false : true);
    }

    private void OnVolumeChanged() {
        _currentAudioSource.volume = _volumeSlider.value;
    }
}
