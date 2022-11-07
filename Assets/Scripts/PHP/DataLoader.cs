using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour {

    public string[] clients;

    IEnumerator Start() {
        WWW clientData = new WWW("http://localhost/MIW_APP/ClientData.php");

        yield return clientData;

        string clientDataString = clientData.text;
        //print(clientDataString);
        clients = clientDataString.Split(';');
        //if (clients[0].Length > 0) {
        //    GetDataValueFromString(clients[0], "Password:");
        //}
    }

    //string GetDataValueFromString(string data, string index) {
    //    string value = data.Substring(data.IndexOf(index) + index.Length);

    //    if (value.Contains("|")) {
    //        value = value.Remove(value.IndexOf("|"));
    //    }

    //    return value;
    //}
}
