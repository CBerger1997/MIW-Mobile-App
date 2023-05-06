using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginMenuView : View, IDataPersistence
{
    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _resetPasswordButton;
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private GameObject _warningText;

    private string username;
    private string password;
    private bool isLoginSuccessful;

    public override void Initialise ()
    {
        _loginButton.onClick.AddListener ( delegate { LoginButtonOnClick (); } );
        _resetPasswordButton.onClick.AddListener ( delegate { ResetPasswordButtonOnClick (); } );
        _warningText.gameObject.SetActive ( false );
    }

    public override void Show ()
    {
        base.Show ();

        if ( username == "" && password == "" )
        {
            isLoginSuccessful = true;
        }
    }

    private void Update ()
    {
        if ( isLoginSuccessful )
        {
            ViewManager.Show<StartUpMenuView> ( false );
        }
    }

    public void LoginButtonOnClick ()
    {
        username = _usernameInput.text;
        password = _passwordInput.text;

        if ( username == "" && password == "" )
        {
            ViewManager.Show<StartUpMenuView> ();
        }
        else
        {
            _warningText.SetActive ( true );
        }
    }

    public void ResetPasswordButtonOnClick ()
    {
        //Input password reset code
    }

    public void LoadData ( UserData data )
    {
        username = data.username;
        password = data.password;
    }

    public void SaveData ( ref UserData data )
    {
        data.username = username;
        data.password = password;
    }
}
