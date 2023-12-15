using System;
using System.Collections.Generic;

[Serializable]
public class CheckInData
{
    public string check_in_date;
    public int feeling;
    public string reason;
}

[Serializable]
public class CheckinList
{
    public List<CheckInData> checkinList;
}
