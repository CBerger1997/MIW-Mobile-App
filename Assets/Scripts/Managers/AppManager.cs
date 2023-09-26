using UnityEngine;

public class AppManager : MonoBehaviour
{

    public static AppManager instance = null;

    public QuoteManager _qManager { get; set; }

    public AffirmationManager _aManager { get; set; }

    private Firebase.FirebaseApp _fbApplication;
    private bool _isFirebaseSet;

    public static AppManager Instance
    {
        get { return instance; }
    }

    private void Awake ()
    {
        // Check for singleton instance and set or destory the instance accordingly
        if ( instance == null )
        {
            instance = this;
        }
        else if ( instance != this )
        {
            Destroy ( gameObject );
        }

        DontDestroyOnLoad ( this );

        _qManager = new QuoteManager ();

        _qManager.ReadQuotesCSV ();

        _aManager = new AffirmationManager ();

        _aManager.ReadRandom ();

        _aManager.ReadSelection ();

        InitialiseFirebase();
    }

    private void InitialiseFirebase ()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync ().ContinueWith ( task =>
        {
            var dependencyStatus = task.Result;
            if ( dependencyStatus == Firebase.DependencyStatus.Available )
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                _fbApplication = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                Debug.Log ( "Firebase is set" );
                _isFirebaseSet = true;
            }
            else
            {
                UnityEngine.Debug.LogError ( System.String.Format (
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus ) );
                // Firebase Unity SDK is not safe to use here.
            }
        } );
    }
}
