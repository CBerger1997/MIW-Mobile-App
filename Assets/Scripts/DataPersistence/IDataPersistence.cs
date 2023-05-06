using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence {
    void LoadData ( UserData data );
    void SaveData ( ref UserData data );
}
