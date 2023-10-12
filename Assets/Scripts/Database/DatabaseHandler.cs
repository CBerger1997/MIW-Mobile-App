using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DatabaseHandler : MonoBehaviour
{
    void Awake ()
    {
        DontDestroyOnLoad ( this.gameObject );
    }

    private void Start ()
    {
        StartCoroutine ( GetUsers () );
    }

    string apiUrl = "https://matthews335.sg-host.com/api/index.php?resource=verify-user&username=charlie.jones@myinternalworld.com&password=$P$Bhy1IsTVIdohmBFld2nmJ1.QdAvM9e0";

    IEnumerator GetUsers ()
    {
        // Create a new UnityWebRequest object.
        UnityWebRequest request = new UnityWebRequest ( apiUrl );

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
            string responseData = request.downloadHandler.text;

            Debug.Log ( responseData );

            // Deserialize the JSON response into a list of users.
            //List<User> users = JsonUtility.FromJson<List<User>> ( responseData );

            // Do something with the users list, such as populate a UI element or create game objects.
        }
    }
}