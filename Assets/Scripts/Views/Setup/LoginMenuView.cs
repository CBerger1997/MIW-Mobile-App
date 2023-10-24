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

    private string username;
    private string password;

    string apiUrl = "https://matthews335.sg-host.com/api/index.php?resource=verify-user";

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

        StartCoroutine ( GetUsers ( username, password ) );
    }

    public void LoginButtonOnClick ()
    {
        if ( _rememberToggle.isOn )
        {
            username = _usernameInput.text;
            password = _passwordInput.text;

            DataPersistenceManager.Instance.SaveUser ();
            DataPersistenceManager.Instance.LoadUser ();
        }

        StartCoroutine ( GetUsers ( _usernameInput.text, _passwordInput.text ) );
    }

    IEnumerator GetUsers ( string username, string password )
    {
        Debug.Log ( "Checking Verification" );

        string userPassText = "&username=" + username + "&password=" + password;

        // Create a new UnityWebRequest object.
        UnityWebRequest request = new UnityWebRequest ( apiUrl + userPassText );

        DownloadHandlerBuffer dH = new DownloadHandlerBuffer ();
        request.downloadHandler = dH;

        // Set the request method to GET.
        request.method = UnityWebRequest.kHttpVerbGET;

        // Send the request and wait for the response.
        yield return request.SendWebRequest ();

        // Check if the request was successful.
        if ( request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError )
        {
            Debug.LogError ( "Failed to get users from API: " + request.error );
        }
        else
        {
            Debug.Log ( request.downloadHandler.text );

            User user = JsonUtility.FromJson<User> ( request.downloadHandler.text );

            Debug.Log ( user.ID );
            Debug.Log ( user.display_name );

            // Get the response data.
            if ( request.downloadHandler.text != "false" )
            {
                ViewManager.Show<StartUpMenuView> ( false );
            }

            // Deserialize the JSON response into a list of users.

            // Do something with the users list, such as populate a UI element or create game objects.
        }
    }

    public void ShowPasswordButtonOnClick ()
    {
        _passwordInput.contentType = _passwordInput.contentType == TMP_InputField.ContentType.Password ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
        _passwordInput.Select ();
    }

    public void ResetPasswordButtonOnClick ()
    {
        //Input password reset code
        Debug.LogError ( "Functionality not yet implemented!" );
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
