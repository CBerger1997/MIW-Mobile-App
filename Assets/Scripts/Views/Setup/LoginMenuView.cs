using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MySql.Data.MySqlClient;

public class LoginMenuView : View, IDataPersistence
{
    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _bypassLoginButton;
    [SerializeField] private Button _resetPasswordButton;
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private GameObject _warningText;
    [SerializeField] private Toggle _rememberToggle;

    private string username;
    private string password;
    private bool isLoginChecked;
    private bool isDataLoaded;

    public override void Initialise ()
    {
        _loginButton.onClick.AddListener ( LoginButtonOnClick );
        _bypassLoginButton.onClick.AddListener ( BypassLoginOnClick );
        _resetPasswordButton.onClick.AddListener ( ResetPasswordButtonOnClick );
        _warningText.gameObject.SetActive ( false );
    }

    private void Update ()
    {
        if ( DatabaseHandler.connection.State == System.Data.ConnectionState.Open && !isLoginChecked && isDataLoaded )
        {
            Debug.Log ( "Checking" );

            string sql = "SELECT user_login AND user_pass FROM wp_users WHERE user_login='" + username + "' AND user_pass='" + password + "' LIMIT 1";
            MySqlCommand cmd = new MySqlCommand ( sql, DatabaseHandler.connection );
            MySqlDataReader rdr = cmd.ExecuteReader ();

            if ( rdr.Read () )
            {
                Debug.Log ( "Found" );
                ViewManager.Show<StartUpMenuView> ( false );
            }


            Debug.Log ( "Not Found" );
            rdr.Close ();
            isLoginChecked = true;
        }
    }

    public void LoginButtonOnClick ()
    {
        string sql = "SELECT user_login AND user_pass FROM wp_users WHERE user_login='" + _usernameInput.text + "' AND user_pass='" + _passwordInput.text + "' LIMIT 1";
        MySqlCommand cmd = new MySqlCommand ( sql, DatabaseHandler.connection );
        MySqlDataReader rdr = cmd.ExecuteReader ();

        if ( rdr.Read () )
        {
            if ( _rememberToggle.isOn )
            {
                username = _usernameInput.text;
                password = _passwordInput.text;

                DataPersistenceManager.Instance.SaveUser ();
                DataPersistenceManager.Instance.LoadUser ();
            }

            rdr.Close ();

            ViewManager.Show<StartUpMenuView> ();
        }
        else
        {
            _warningText.SetActive ( true );
        }

        rdr.Close ();
    }

    void BypassLoginOnClick ()
    {
        ViewManager.Show<StartUpMenuView> ();
    }

    public void ResetPasswordButtonOnClick ()
    {
        //Input password reset code
    }

    public void LoadData ( UserData data )
    {
        username = data.username;
        password = data.password;
        isDataLoaded = true;
    }

    public void SaveData ( ref UserData data )
    {
        data.username = username;
        data.password = password;
    }
}
