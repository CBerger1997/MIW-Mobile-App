using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuView : View {

    [SerializeField] private Button _notificationsButton;
    [SerializeField] private Button _declarationsButton;
    [SerializeField] private Button _deleteUserButton;
    [SerializeField] private Button _changePasswordButton;

    public override void Initialise () {
        _deleteUserButton.onClick.AddListener ( DeleteUserDataOnClick );
        _notificationsButton.onClick.AddListener ( () => ViewManager.Show<NotificationsView> () );
        _declarationsButton.onClick.AddListener ( () => ViewManager.Show<DeclarationsView> () );
        _changePasswordButton.onClick.AddListener ( () => ViewManager.Show<ChangePasswordView> () );
    }

    private void DeleteUserDataOnClick () {
        AppManager.instance._uData = new UserData ();
        AppManager.instance.SaveUserData ();
    }
}