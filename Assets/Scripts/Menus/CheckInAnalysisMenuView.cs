using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckInAnalysisMenuView : View {
    [SerializeField] private Button _backButton;

    private GameObject _calendarButton;

    public override void Initialise() {
        _backButton.onClick.AddListener(() => ViewManager.ShowLast());
    }
}
