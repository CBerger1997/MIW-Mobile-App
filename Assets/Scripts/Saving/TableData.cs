using System.Collections.Generic;
using UnityEngine;

public class TableData {
    [SerializeField] public string username;
    [SerializeField] public string password;

    [System.Serializable]
    public class CheckInClass {
        public int _emotion;
        public int _reason;
        public string _timeStamp;

        public CheckInClass() {
            _emotion = 0;
            _reason = 0;
            _timeStamp = "";
        }

        public CheckInClass(int e, int r, string time) {
            _emotion = e;
            _reason = r;
            _timeStamp = time;
        }
    }

    [SerializeField] public List<CheckInClass> _checkInData;

    public TableData() {
        username = "";
        password = "";

        _checkInData = new List<CheckInClass>();
    }
}
