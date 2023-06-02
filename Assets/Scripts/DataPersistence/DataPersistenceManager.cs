using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header ( "File Storage Config" )]

    [SerializeField] private string fileName;

    private UserData userData;

    private List<IDataPersistence> dataPersistences;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager Instance { get; private set; }

    private void Awake ()
    {
        if ( Instance != null )
        {
            Debug.LogError ( "A Data persistance Manager already exists within this scene" );
        }

        Instance = this;

        this.dataHandler = new FileDataHandler ( Application.persistentDataPath, fileName );
        this.dataPersistences = FindAllDataPersistences ();

        LoadUser ();
    }

    public void NewData ()
    {
        this.userData = new UserData ();
    }

    public void LoadUser ()
    {
        // Load any saved data from a file using the data handler
        this.userData = dataHandler.Load ();

        // If no data can be loaded, initialise to a new user
        if ( this.userData == null )
        {
            Debug.Log ( "No data was found. Initialising data to defaults" );
            NewData ();
        }

        // Push the loaded data to all other scripts that need it
        foreach ( IDataPersistence persistence in dataPersistences )
        {
            persistence.LoadData ( userData );
        }
    }

    public void SaveUser ()
    {
        // Pass data to other scripts so they can update it
        foreach ( IDataPersistence persistence in dataPersistences )
        {
            persistence.SaveData ( ref userData );
        }

        // Save that data to a file using the data handler
        dataHandler.Save ( userData );
    }

    private void OnApplicationPause ()
    {
        SaveUser ();
    }

    private void OnApplicationQuit () {
        SaveUser ();
    }

    private List<IDataPersistence> FindAllDataPersistences ()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour> ().OfType<IDataPersistence> ();

        return new List<IDataPersistence> ( dataPersistences );
    }
}
