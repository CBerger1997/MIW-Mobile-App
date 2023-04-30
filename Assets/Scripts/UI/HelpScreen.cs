using UnityEngine;
using UnityEngine.UI;

public class HelpScreen : MonoBehaviour {
    [SerializeField] private Button _helpButton;
    [SerializeField] private GameObject _helpScreen;
    [SerializeField] private GameObject _toggleScreen;
    [SerializeField] private Button _closeButton;

    public void ConfigureHelpScreen () {
        _helpButton.onClick.AddListener ( ToggleHelpMenu );
        _closeButton.onClick.AddListener ( ToggleHelpMenu );
        _helpScreen.SetActive ( false );
    }

    public void ToggleHelpMenu () {
        if ( _helpScreen.activeSelf ) {
            ToggleOffHelpMenu ();
        } else {
            ToggleOnHelpMenu ();
        }
    }

    public void ToggleOffHelpMenu () {
        _toggleScreen.SetActive ( true );
        _helpScreen.SetActive ( false );
    }

    public void ToggleOnHelpMenu () {
        _toggleScreen.SetActive ( false );
        _helpScreen.SetActive ( true );
    }
}
