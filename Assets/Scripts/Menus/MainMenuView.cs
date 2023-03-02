using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MainMenuView : View {
    [SerializeField] private Button _affirmationButton;
    [SerializeField] private Button _visualisationsButton;
    [SerializeField] private Button _justBreathButton;
    [SerializeField] private Button _journalButton;
    [SerializeField] private Button _checkInButton;
    [SerializeField] private AutoSpaceOnResolution _autoSpaceOnResolution;

    public override void Initialise() {
        _affirmationButton.onClick.AddListener(() => ViewManager.Show<AffirmationMenuView>());

        _visualisationsButton.onClick.AddListener(() => ViewManager.Show<VisualisationsMenuView>());

        _justBreathButton.onClick.AddListener(() => ViewManager.Show<JustBreathMenuView>());

        _journalButton.onClick.AddListener(() => ViewManager.Show<JournalMenuView>());

        _checkInButton.onClick.AddListener(() => ViewManager.Show<CheckInMoodMenu>());

        _autoSpaceOnResolution.PerformAutoSpace();
    }
}