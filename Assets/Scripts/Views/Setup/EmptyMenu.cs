using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyMenu : View, IDataPersistence
{
    private string username;
    private string password;

    public override void Initialise ()
    {

    }

    public override void Show ()
    {
        base.Show ();

        StartCoroutine ( DatabaseHandler.GetUsers ( username, password ) );
    }

    public void LoadData ( UserData data )
    {
        username = data.username;
        password = data.password;
    }

    public void SaveData ( ref UserData data )
    {

    }
}
