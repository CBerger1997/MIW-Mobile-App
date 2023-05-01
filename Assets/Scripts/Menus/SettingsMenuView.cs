using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuView : View {

    [SerializeField] private Button _deleteUserButton;
    [SerializeField] private Button _changePasswordButton;

    public override void Initialise () {
        _deleteUserButton.onClick.AddListener ( DeleteUserDataOnClick );
        _changePasswordButton.onClick.AddListener ( () => ViewManager.Show<ChangePasswordView> () );
    }

    private void DeleteUserDataOnClick () {
        AppManager.instance._uData = new UserData ();
        AppManager.instance.SaveUserData ();
    }
}