using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;

public class FileDataHandler
{
    private string dataDirPath = "";

    private string dataFileName = "";

    public FileDataHandler ( string dataDirPath, string dataFileName )
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public UserData Load ()
    {
        //Path.Combine accounts for different OS's having different path separators
        string fullPath = Path.Combine ( dataDirPath, dataFileName );

        UserData loadedData = null;

        if ( File.Exists ( fullPath ) )
        {
            try
            {
                // Load the serialized data from the file
                string dataToLoad = "";

                using ( FileStream stream = new FileStream ( fullPath, FileMode.Open ) )
                {
                    using ( StreamReader reader = new StreamReader ( stream ) )
                    {
                        dataToLoad = reader.ReadToEnd ();
                    }
                }

                // Deserialise the data from Json back into the C# object
                loadedData = JsonUtility.FromJson<UserData> ( dataToLoad );
            }
            catch ( Exception e )
            {
                Debug.LogError ( "Error occured when trying to load data from file: " + fullPath + "\n" + e );
            }
        }

        return loadedData;
    }

    public void Save ( UserData data )
    {
        Debug.Log ( data.password );
        //Path.Combine accounts for different OS's having different path separators
        string fullPath = Path.Combine ( dataDirPath, dataFileName );

        try
        {
            // Create the directory the file will be written to if it doesn't already exist
            Directory.CreateDirectory ( Path.GetDirectoryName ( fullPath ) );

            // Serialise the C~ user data object in Json
            string dataToStore = JsonUtility.ToJson ( data, true );

            // Write the serialised data to the file
            using ( FileStream stream = new FileStream ( fullPath, FileMode.Create ) )
            {
                using ( StreamWriter writer = new StreamWriter ( stream ) )
                {
                    writer.Write ( dataToStore );
                }
            }
        }
        catch ( Exception e )
        {
            Debug.LogError ( "Error occured when trying to save data to file: " + fullPath + "\n" + e );
        }
    }

    public void Delete() 
    {
        //Path.Combine accounts for different OS's having different path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try 
        {
            File.Delete(fullPath);
        }
        catch ( Exception e ) 
        {
            Debug.LogError("Error occured when trying to delete data to file: " + fullPath + "\n" + e);
        }
    }
}
