using UnityEngine;
using System;
//using System.Data;
//using System.Text;

//using System.Collections;
//using System.Collections.Generic;
//using System.Security.Cryptography;

//using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.Networking;

public class DatabaseHandler : MonoBehaviour
{
    private string ip = "35.214.78.242";
    private string db_username = "u8o1ebm9g8pig";
    private string db_password = "szgrevzuh2vi";
    private string db = "dbh5k8auqjvkwb";

    public static MySqlConnection connection;

    void Awake ()
    {
        DontDestroyOnLoad ( this.gameObject );

        string connectionstring = "SERVER=" + ip + ";" + "DATABASE=" + db + ";" + "UID=" + db_username + ";" + "PASSWORD=" + db_password + ";";

        connection = new MySqlConnection ( connectionstring );
        connection.Open ();
    }

    private void Start ()
    {
        GetUsers ();
    }

    public string apiUrl = "https://matthews335.sg-host.com/api/users.py";

    public void GetUsers ()
    {
        Debug.Log ( "Getting Users" );
        // Create a new UnityWebRequest object.
        UnityWebRequest request = new UnityWebRequest ( apiUrl );

        // Set the request method to GET.
        request.method = UnityWebRequest.kHttpVerbGET;

        // Send the request and wait for the response.
        request.SendWebRequest ();

        // Check if the request was successful.
        if ( request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError )
        {
            Debug.LogError ( "Failed to get users from API: " + request.error );
            return;
        }

        // Get the response data.
        string responseData = request.downloadHandler.text;

        Debug.Log ( "RESPONSE!" );
        Debug.Log ( responseData );

        // Deserialize the JSON response into a list of users.
        //List<User> users = JsonUtility.FromJson<List<User>> ( responseData );

        // Do something with the users list, such as populate a UI element or create game objects.
    }
}