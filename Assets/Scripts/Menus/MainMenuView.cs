using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : View {
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _feelingsButton;

    public override void Initialise() {
        _settingsButton.onClick.AddListener(() => ViewManager.Show<SettingsMenuView>());

        _feelingsButton.onClick.AddListener(() => ViewManager.Show<FeelingMenuView>());
    }
}