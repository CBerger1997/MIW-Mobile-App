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
}
