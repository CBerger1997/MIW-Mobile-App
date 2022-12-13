using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualisationsMenuView : View {

    [SerializeField] private Button _backButton;

    [SerializeField] private List<Button> _audioButtons;

    [SerializeField] private AudioSource _currentAudioSource;

    public override void Initialise() {
        _backButton.onClick.AddListener(() => ViewManager.ShowLast());

        for (int i = 0; i < _audioButtons.Count; i++) {
            int copy = i;
            _audioButtons[copy].onClick.AddListener(() => OnAudioButtonClick(copy));
        }
    }

    private void OnAudioButtonClick(int index) {
        if (_currentAudioSource != null && _currentAudioSource.isPlaying) {
            _currentAudioSource.Stop();
        }

        _audioButtons[index].GetComponent<AudioSource>().Play();
        _currentAudioSource = _audioButtons[index].GetComponent<AudioSource>();

    }
}
