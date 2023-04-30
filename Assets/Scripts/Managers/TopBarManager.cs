using UnityEngine;
using UnityEngine.UI;

public class TopBarManager : MonoBehaviour {
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _crisisButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _backButton;

    private void Awake () {
        this.gameObject.SetActive ( false );
    }

    void Start () {
        _homeButton.onClick.AddListener ( delegate { ViewManager.Show<MainMenuView> ( false ); ViewManager.ClearHistory (); } );

        _crisisButton.onClick.AddListener ( () => ViewManager.Show<CrisisMenuView> () );

        _settingsButton.onClick.AddListener ( () => ViewManager.Show<SettingsMenuView> () );

        _backButton.onClick.AddListener ( () => ViewManager.ShowLast () );
    }

    public void AllowBackButton ( bool isAllow ) {
        if ( isAllow ) {
            _backButton.gameObject.SetActive ( true );
        } else {
            _backButton.gameObject.SetActive ( false );
        }
    }
}
