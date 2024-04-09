using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LoginMenuView : View, IDataPersistence
{
    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _resetPasswordButton;
    [SerializeField] private Button _showPasswordButton;
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private GameObject _warningText;
    [SerializeField] private Toggle _rememberToggle;
    [SerializeField] private DatabaseHandler _databaseHandler;

    private string username;
    private string password;
    public override void Initialise ()
    {
        _loginButton.onClick.AddListener ( LoginButtonOnClick );
        _resetPasswordButton.onClick.AddListener ( ResetPasswordButtonOnClick );
        _showPasswordButton.onClick.AddListener ( ShowPasswordButtonOnClick );
        _warningText.gameObject.SetActive ( false );
    }

    public override void Show ()
    {
        base.Show ();
    }

    private void LoginButtonOnClick ()
    {
        if ( _rememberToggle.isOn )
        {
            username = _usernameInput.text;
            password = _passwordInput.text;

            DataPersistenceManager.Instance.SaveUser ();
            DataPersistenceManager.Instance.LoadUser ();
        }

        StartCoroutine ( DatabaseHandler.GetUsers ( _usernameInput.text, _passwordInput.text ) );
    }

    private void ShowPasswordButtonOnClick ()
    {
        _passwordInput.contentType = _passwordInput.contentType == TMP_InputField.ContentType.Password ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
        _passwordInput.Select ();
    }

    private void ResetPasswordButtonOnClick ()
    {
        //Input password reset code
        Debug.LogError ( "Functionality not yet implemented!" );
    }

    public void LoadData ( UserData data )
    {

    }

    public void SaveData ( ref UserData data )
    {
        data.username = username;
        data.password = password;
    }
}
