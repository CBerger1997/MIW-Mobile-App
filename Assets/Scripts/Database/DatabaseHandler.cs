using UnityEngine;
using System;
using System.Data;
using System.Text;

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

using MySql.Data;
using MySql.Data.MySqlClient;
using UnityEngine.Networking;

public class DatabaseHandler : MonoBehaviour
{
    public string host, database, user, password;
    public bool pooling = true;

    private string connectionString;
    private MySqlConnection con = null;
    private MySqlCommand cmd = null;
    private MySqlDataReader rdr = null;

    private MD5 _md5Hash;

    string LoginFile = "https://matthews335.sg-host.com/user_login.php?";
    string email = "";


    void Awake ()
    {
        DontDestroyOnLoad ( this.gameObject );


        string username = "charlie.jones@myinternalworld.com";

        string password = "$P$Bhy1IsTVIdohmBFld2nmJ1.QdAvM9e0";


        string status = "";

        UnityWebRequest login = new UnityWebRequest ( LoginFile );

        StartCoroutine ( LoginUser ( username, password ) );

    }

    IEnumerator LoginUser ( string username, string password )
    {
        UnityWebRequest login = new UnityWebRequest ( LoginFile );

        yield return login.SendWebRequest ();

        Debug.Log ( "Start Login" );

        if ( login.result == UnityWebRequest.Result.ConnectionError )
        {
            Debug.LogError ( "Login Failed" );
            Debug.Log ( login.error );
        }
        else
        {
            Debug.Log ( "Start Login 2" );

            string[] credentials = login.result.ToString ().Split ( '/' );

            foreach ( string str in credentials )
            {
                Debug.Log ( "Start Login 3" + str );
                Debug.Log ( login.error );

                string[] creds = str.Split ( '=' );

                for ( int i = 0; i < creds.Length; i++ )
                {
                    if ( creds[ i ] == "email" )
                    {
                        email = creds[ i + 1 ];
                    }
                }
            }
        }
    }

    void onApplicationQuit ()
    {
        if ( con != null )
        {
            if ( con.State.ToString () != "Closed" )
            {
                con.Close ();
                Debug.Log ( "Mysql connection closed" );
            }
            con.Dispose ();
        }
    }

    public string getFirstShops ()
    {
        using ( rdr = cmd.ExecuteReader () )
        {
            while ( rdr.Read () )
            {
                return rdr[ 0 ] + " -- " + rdr[ 1 ];
            }
        }
        return "empty";
    }
    public string GetConnectionState ()
    {
        return con.State.ToString ();
    }
}
