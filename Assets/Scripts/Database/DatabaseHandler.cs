using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class DatabaseHandler : MonoBehaviour
{
    User user = new User ();

    public IEnumerator GetUsers ( string username, string password )
    {
        string apiUrl = "https://matthews335.sg-host.com/api/index.php?resource=verify-user";

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
            user = JsonUtility.FromJson<User> ( request.downloadHandler.text );

            // Get the response data.
            if ( request.downloadHandler.text != "false" )
            {
                ViewManager.Show<StartUpMenuView> ( false );
            }
        }
    }


    public IEnumerator CheckInUser ( string feeling, string reason )
    {
        Debug.Log ( "Setting URL" );

        string apiUrl = "https://matthews335.sg-host.com/api/index.php?resource=checkin-user";

        string checkInText =
            "&id=" + user.ID +
            "&time=" + DateTime.Now.ToString ( "yyyy-MM-dd HH:mm:ss" ) +
            "&date=" + DateTime.Now.ToString ( "yyyy-MM-dd" ) +
            "&feeling=" + feeling +
            "&reason=" + reason;


        // Create a new UnityWebRequest object.
        UnityWebRequest request = new UnityWebRequest ( apiUrl + checkInText );

        DownloadHandlerBuffer dH = new DownloadHandlerBuffer ();
        request.downloadHandler = dH;

        // Set the request method to GET.
        request.method = UnityWebRequest.kHttpVerbPOST;

        // Send the request and wait for the response.
        yield return request.SendWebRequest ();

        Debug.Log ( "URL Response" );

        // Check if the request was successful.
        if ( request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError )
        {
            Debug.LogError ( "Failed to get users from API: " + request.error );
        }
        else
        {
            Debug.LogError ( "Checkin Save Failed" );

            ErrorMessageManager.ActivateErrorMessage ( "Checkin Save Failed: please check internet connection and try again" );

            DataPersistenceManager.Instance.SaveUser ();
            DataPersistenceManager.Instance.LoadUser ();

            ViewManager.Show<MainMenuView> ( false );

            //// Get the response data.
            //if ( request.downloadHandler.text == "false" )
            //{
            //    Debug.LogError ( "Checkin Save Failed" );

            //    ErrorMessageManager.ActivateErrorMessage ( "Checkin Save Failed: please check internet connection and try again" );

            //    DataPersistenceManager.Instance.SaveUser ();
            //    DataPersistenceManager.Instance.LoadUser ();

            //    ViewManager.Show<MainMenuView> ( false );
            //}
            //else
            //{
            //    Debug.Log ( request.downloadHandler.text );

            //    DataPersistenceManager.Instance.SaveUser ();
            //    DataPersistenceManager.Instance.LoadUser ();

            //    ViewManager.Show<MainMenuView> ( false );
            //}
        }
    }
}