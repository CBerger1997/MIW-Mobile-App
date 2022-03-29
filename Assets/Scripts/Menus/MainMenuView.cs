using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : View {
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _crisisButton;
    [SerializeField] private Button _affirmationButton;
    [SerializeField] private Button _visualisationsButton;
    [SerializeField] private Button _justBreathButton;
    [SerializeField] private Button _journalButton;
    [SerializeField] private Button _checkInButton;

    public override void Initialise() {
        _settingsButton.onClick.AddListener(() => ViewManager.Show<SettingsMenuView>());

        _crisisButton.onClick.AddListener(() => ViewManager.Show<CrisisMenuView>());

        _affirmationButton.onClick.AddListener(() => ViewManager.Show<AffirmationMenuView>());

        _visualisationsButton.onClick.AddListener(() => ViewManager.Show<VisualisationsMenuView>());

        _justBreathButton.onClick.AddListener(() => ViewManager.Show<JustBreathMenuView>());

        _journalButton.onClick.AddListener(() => ViewManager.Show<JournalMenuView>());

        _checkInButton.onClick.AddListener(() => ViewManager.Show<CheckInMenuView>());
    }

    public override void Show() {
        base.Show();

        if(AppManager.instance._uData.hasUserCheckedIn) {
            _checkInButton.GetComponent<Image>().color = Color.green;
            _checkInButton.enabled = false;
        }
    }
}