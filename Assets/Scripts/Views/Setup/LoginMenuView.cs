using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MySql.Data.MySqlClient;
using UnityEngine.Networking;
using System.Collections;

public class LoginMenuView : View, IDataPersistence
{
    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _resetPasswordButton;
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private GameObject _warningText;
    [SerializeField] private Toggle _rememberToggle;

    private string username;
    private string password;
    private bool isLoginChecked;
    private bool isDataLoaded;

    string apiUrl = "https://matthews335.sg-host.com/api/index.php?resource=verify-user";

    public override void Initialise ()
    {
        _loginButton.onClick.AddListener ( LoginButtonOnClick );
        _resetPasswordButton.onClick.AddListener ( ResetPasswordButtonOnClick );
        _warningText.gameObject.SetActive ( false );

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
            // Get the response data.
            if ( request.downloadHandler.text == "true" )
            {
                ViewManager.Show<StartUpMenuView> ( false );
            }

            // Deserialize the JSON response into a list of users.
            //List<User> users = JsonUtility.FromJson<List<User>> ( responseData );

            // Do something with the users list, such as populate a UI element or create game objects.
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
        isDataLoaded = true;
    }

    public void SaveData ( ref UserData data )
    {
        data.username = username;
        data.password = password;
    }
}
