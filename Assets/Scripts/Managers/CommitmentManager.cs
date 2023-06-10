using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommitmentManager : MonoBehaviour, IDataPersistence
{
    public bool _commitmentExists { get; set; } = false;

    public void LoadData ( UserData data )
    {
        _commitmentExists = data.commitmentExists;
    }

    public void SaveData ( ref UserData data )
    {
 
    }
}
