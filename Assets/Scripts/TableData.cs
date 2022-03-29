using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableData {
    [SerializeField] public string username;
    [SerializeField] public string password;

    public TableData() {
        username = "callum";
        password = "password1234";
    }
}
