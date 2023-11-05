using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using static UnityEngine.EventSystems.EventTrigger;

public class DatabaseHandler : MonoBehaviour
{
    private static DatabaseHandler s_instance;

    User user = new User ();

    private void Awake ()
    {
        s_instance = this;
    }

    public static IEnumerator GetUsers ( string username, string password )
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
            s_instance.user = JsonUtility.FromJson<User> ( request.downloadHandler.text );

            // Get the response data.
            if ( request.downloadHandler.text != "false" )
            {
                ViewManager.Show<StartUpMenuView> ( false );
            }
        }
    }

    public static IEnumerator CheckInUser ( string feeling, string reason )
    {
        Debug.Log ( "Setting URL" );

        string apiUrl = "https://matthews335.sg-host.com/api/index.php?resource=checkin-user";

        string checkInText =
            "&id=" + s_instance.user.ID +
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

            ErrorMessageManager.ActivateErrorMessage ( "Checkin Save Failed: you appear to have no internet connection, please try again" );
        }
        else
        {
            // Get the response data.
            if ( request.downloadHandler.text == "false" )
            {
                Debug.LogError ( "Checkin DB Input Failed" );

                ErrorMessageManager.ActivateErrorMessage ( "Checkin Save Failed: some information appears to be incorrect, please let Inner Calm know" );

                DataPersistenceManager.Instance.SaveUser ();
                DataPersistenceManager.Instance.LoadUser ();

                ViewManager.Show<MainMenuView> ( false );
            }
            else
            {
                Debug.Log ( request.downloadHandler.text );

                DataPersistenceManager.Instance.SaveUser ();
                DataPersistenceManager.Instance.LoadUser ();

                ViewManager.Show<MainMenuView> ( false );
            }
        }
    }

    public static IEnumerator JournalUser ( string entry )
    {
        string apiUrl = "https://matthews335.sg-host.com/api/index.php?resource=journal-user";

        string journalText =
            "&id=" + s_instance.user.ID +
            "&entry=" + entry +
            "&time=" + DateTime.Now.ToString ( "yyyy-MM-dd HH:mm:ss" ) +
            "&date=" + DateTime.Now.ToString ( "yyyy-MM-dd" );

        // Create a new UnityWebRequest object.
        UnityWebRequest request = new UnityWebRequest ( apiUrl + journalText );

        DownloadHandlerBuffer dH = new DownloadHandlerBuffer ();
        request.downloadHandler = dH;

        // Set the request method to GET.
        request.method = UnityWebRequest.kHttpVerbPOST;

        // Send the request and wait for the response.
        yield return request.SendWebRequest ();

        // Check if the request was successful.
        if ( request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError )
        {
            Debug.LogError ( "Failed to get users from API: " + request.error );

            ErrorMessageManager.ActivateErrorMessage ( "Checkin Save Failed: you appear to have no internet connection, please try again" );
        }
        else
        {
            // Get the response data.
            if ( request.downloadHandler.text == "false" )
            {
                Debug.LogError ( "Checkin DB Input Failed" );

                ErrorMessageManager.ActivateErrorMessage ( "Checkin Save Failed: some information appears to be incorrect, please let Inner Calm know" );

                DataPersistenceManager.Instance.SaveUser ();
                DataPersistenceManager.Instance.LoadUser ();

                ViewManager.ShowLast ();
            }
            else
            {
                Debug.Log ( request.downloadHandler.text );

                DataPersistenceManager.Instance.SaveUser ();
                DataPersistenceManager.Instance.LoadUser ();

                ViewManager.ShowLast ();
            }
        }
    }

    public static IEnumerator GetUserCheckinData ()
    {
        string apiUrl = "https://matthews335.sg-host.com/api/index.php?resource=journal-user";

        string checkinText = "&id=" + s_instance.user.ID;

        // Create a new UnityWebRequest object.
        UnityWebRequest request = new UnityWebRequest ( apiUrl + journalText );

        DownloadHandlerBuffer dH = new DownloadHandlerBuffer ();
        request.downloadHandler = dH;

        // Set the request method to GET.
        request.method = UnityWebRequest.kHttpVerbPOST;

        // Send the request and wait for the response.
        yield return request.SendWebRequest ();

        // Check if the request was successful.
        if ( request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError )
        {
            Debug.LogError ( "Failed to get users from API: " + request.error );

            ErrorMessageManager.ActivateErrorMessage ( "Checkin Save Failed: you appear to have no internet connection, please try again" );
        }
        else
        {
            // Get the response data.
            if ( request.downloadHandler.text == "false" )
            {
                Debug.LogError ( "Checkin DB Input Failed" );

                ErrorMessageManager.ActivateErrorMessage ( "Checkin Save Failed: some information appears to be incorrect, please let Inner Calm know" );

                DataPersistenceManager.Instance.SaveUser ();
                DataPersistenceManager.Instance.LoadUser ();

                ViewManager.ShowLast ();
            }
            else
            {
                Debug.Log ( request.downloadHandler.text );

                DataPersistenceManager.Instance.SaveUser ();
                DataPersistenceManager.Instance.LoadUser ();

                ViewManager.ShowLast ();
            }
        }
    }
}