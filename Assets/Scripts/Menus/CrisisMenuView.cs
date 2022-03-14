using UnityEngine;
using UnityEngine.UI;

public class CrisisMenuView : View {

    [SerializeField] private Button _backButton;

    public override void Initialise() {
        _backButton.onClick.AddListener(() => ViewManager.ShowLast());
    }
}
