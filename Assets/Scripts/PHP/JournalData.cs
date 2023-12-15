using System.Collections.Generic;
using System;

[Serializable]
public class JournalData
{
    public string journal_entry;
    public string journal_time;
    public string journal_date;

    public JournalData () { }
}

[Serializable]
public class JournalList
{
    public List<JournalData> journalList;

    public JournalList () { }
}