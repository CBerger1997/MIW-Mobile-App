using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using static UnityEngine.EventSystems.EventTrigger;
using System.Collections.Generic;

public class DatabaseHandler : MonoBehaviour
{
    private static DatabaseHandler s_instance;

    User user = new User ();

    private void Awake ()
    {
        s_instance = this;
    }

    /// <summary>
    /// Checks user credentials for log in, keeps a hold of the user id and display name
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
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
            Debug.Log ( request.downloadHandler.text );

            s_instance.user = JsonUtility.FromJson<User> ( request.downloadHandler.text );

            // Get the response data.
            if ( request.downloadHandler.text != "false" )
            {
                ViewManager.Show<StartUpMenuView> ( false );
            }
        }
    }

    #region CHECKIN DATABASE

    /// <summary>
    /// Sends user check in data to the database
    /// </summary>
    /// <param name="feeling"></param>
    /// <param name="reason"></param>
    /// <returns></returns>
    public static IEnumerator CheckInUser ( string feeling, string reason )
    {
        Debug.Log ( "Setting URL" );

        string apiUrl = "https://matthews335.sg-host.com/api/index.php?resource=checkin-user";

        string checkInText =
            "&id=" + s_instance.user.user_id +
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
            Debug.LogError ( "Failed to get request from API: " + request.error );

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

    /// <summary>
    /// Gets user check in data from the database for check in history
    /// </summary>
    /// <returns></returns>
    public static IEnumerator CheckInHistory ()
    {
        Debug.Log ( "Setting URL" );

        string apiUrl = "https://matthews335.sg-host.com/api/index.php?resource=checkin-history";

        string checkInText = "&id=" + s_instance.user.user_id;

        Debug.Log ( "ID: " + s_instance.user.user_id );

        // Create a new UnityWebRequest object.
        UnityWebRequest request = new UnityWebRequest ( apiUrl + checkInText );

        DownloadHandlerBuffer dH = new DownloadHandlerBuffer ();
        request.downloadHandler = dH;

        // Set the request method to GET.
        request.method = UnityWebRequest.kHttpVerbGET;

        // Send the request and wait for the response.
        yield return request.SendWebRequest ();

        Debug.Log ( "URL Response" );

        // Check if the request was successful.
        if ( request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError )
        {
            Debug.LogError ( "Failed to get request from API: " + request.error );

            ErrorMessageManager.ActivateErrorMessage ( "Checkin History Failed: you appear to have no internet connection, please try again" );

            ViewManager.ShowLast ();
        }
        else
        {
            // Get the response data.
            if ( request.downloadHandler.text == "false" )
            {
                Debug.LogError ( "Checkin DB Request Failed" );

                ErrorMessageManager.ActivateErrorMessage ( "Checkin History Failed: some information appears to be incorrect, please let Inner Calm know" );

                ViewManager.ShowLast ();
            }
            else
            {
                Debug.Log ( request.downloadHandler.text );

                CheckinList checkinList = JsonUtility.FromJson<CheckinList> ( request.downloadHandler.text );

                Debug.Log ( checkinList.checkinList.Count );

                foreach ( CheckInData data in checkinList.checkinList )
                {
                    Debug.Log ( "Date: " + data.check_in_date + ", Emotion: " + data.feeling + ", Reason: " + data.reason );
                }

                ViewManager.GetView<CheckInAnalysisMenuView> ().UpdateCheckInData ( checkinList.checkinList );
                ViewManager.GetView<CheckInAnalysisMenuView> ().UpdateCalendar ( DateTime.Now.Year, DateTime.Now.Month );

            }
        }
    }

    #endregion //CHECKIN DATABASE

    /// <summary>
    /// Sends a journal entry to the database
    /// </summary>
    /// <param name="entry"></param>
    /// <returns></returns>
    public static IEnumerator JournalUser ( string entry )
    {
        string apiUrl = "https://matthews335.sg-host.com/api/index.php?resource=user-journal-entry";

        string journalText =
            "&id=" + s_instance.user.user_id +
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
            Debug.LogError ( "Failed to get request from API: " + request.error );

            ErrorMessageManager.ActivateErrorMessage ( "Journal Save Failed: you appear to have no internet connection, please try again" );
        }
        else
        {
            // Get the response data.
            if ( request.downloadHandler.text == "false" )
            {
                Debug.LogError ( "Journal DB Input Failed" );

                ErrorMessageManager.ActivateErrorMessage ( "Journal Save Failed: some information appears to be incorrect, please let Inner Calm know" );

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

    /// <summary>
    /// Returns the user check in data from the database to populate the check in (history) calendar view
    /// </summary>
    /// <returns></returns>
    public static IEnumerator GetUserJournalData ()
    {
        string apiUrl = "https://matthews335.sg-host.com/api/index.php?resource=user-journal-history";

        string checkinText = "&id=" + s_instance.user.user_id;

        // Create a new UnityWebRequest object.
        UnityWebRequest request = new UnityWebRequest ( apiUrl + checkinText );

        DownloadHandlerBuffer dH = new DownloadHandlerBuffer ();
        request.downloadHandler = dH;

        // Set the request method to GET.
        request.method = UnityWebRequest.kHttpVerbGET;

        // Send the request and wait for the response.
        yield return request.SendWebRequest ();

        // Check if the request was successful.
        if ( request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError )
        {
            Debug.LogError ( "Failed to get request from API: " + request.error );

            ErrorMessageManager.ActivateErrorMessage ( "Journal History Failed: you appear to have no internet connection, please try again" );
        }
        else
        {
            // Get the response data.
            if ( request.downloadHandler.text == "false" )
            {
                Debug.LogError ( "Journal DB Request Failed" );

                ErrorMessageManager.ActivateErrorMessage ( "Journal History Failed: some information appears to be incorrect, please let Inner Calm know" );

                ViewManager.ShowLast ();
            }
            else
            {
                Debug.Log ( request.downloadHandler.text );

                JournalList journalList = JsonUtility.FromJson<JournalList> ( request.downloadHandler.text );

                Debug.Log ( journalList.journalList.Count );

                ViewManager.GetView<JournalPastView> ().SetPastEntries ( journalList.journalList );
                ViewManager.GetView<JournalPastView> ().PopulatePastEntries ();
            }
        }
    }
}