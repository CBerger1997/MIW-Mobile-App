using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.Tilemaps;

public class CheckInAnalysisMenuView : View {
    public class Day {
        public int dayNum;
        public Color dayColour;
        public GameObject obj;
        public int userDataIndex;

        public Day ( int dayNum, Color dayColour, GameObject obj ) {
            this.dayNum = dayNum;
            this.dayColour = dayColour;
            this.obj = obj;
            this.userDataIndex = -1;
            obj.GetComponent<Image> ().color = dayColour;
            UpdateDay ( dayNum );
        }

        public void UpdateColour ( Color newColor ) {
            obj.GetComponent<Image> ().color = newColor;
            dayColour = newColor;
        }

        public void UpdateDay ( int newDayNum ) {
            this.dayNum = newDayNum;

            if ( dayColour == Color.white || dayColour == Color.green ) {
                obj.GetComponentInChildren<TMP_Text> ().text = ( dayNum + 1 ).ToString ();
            } else {
                obj.GetComponentInChildren<TMP_Text> ().text = "";
            }
        }
    }

    [SerializeField] private Transform[] Weeks;
    [SerializeField] private TMP_Text MonthAndYear;
    [SerializeField] private GameObject CheckInTextPanel;

    private DateTime curDate = DateTime.Now;
    private List<Day> days = new List<Day> ();

    private List<TMP_Text> checkInTexts = new List<TMP_Text> ();

    private List<string> moods = new List<string> ();
    private List<string> contexts = new List<string> ();

    public override void Initialise () {
        UpdateCalendar ( DateTime.Now.Year, DateTime.Now.Month );

        foreach ( TMP_Text text in CheckInTextPanel.GetComponentsInChildren<TMP_Text> () ) {
            checkInTexts.Add ( text );
        }

        foreach ( Day day in days ) {
            day.obj.GetComponent<Button> ().onClick.AddListener ( delegate { OnDateButtonClicked ( day ); } );
        }

        ReadInTextData ();
    }

    private void UpdateCalendar ( int year, int month ) {
        DateTime temp = new DateTime ( year, month, 1 );
        curDate = temp;
        MonthAndYear.text = temp.ToString ( "MMMM" ) + " " + temp.Year.ToString ();
        int startDay = GetMonthStartDay ( year, month );
        int totalDays = GetTotalNumberOfDays ( year, month );

        startDay--;

        if ( startDay < 0 ) {
            startDay = 6;
        }

        if ( days.Count == 0 ) {
            for ( int w = 0; w < 6; w++ ) {
                for ( int i = 0; i < 7; i++ ) {
                    Day newDay;
                    int curDay = ( w * 7 ) + i;
                    if ( curDay < startDay || curDay - startDay >= totalDays ) {
                        newDay = new Day ( curDay - startDay, Color.grey, Weeks[ w ].GetChild ( i ).gameObject );
                    } else {
                        newDay = new Day ( curDay - startDay, Color.white, Weeks[ w ].GetChild ( i ).gameObject );
                    }
                    days.Add ( newDay );
                }
            }
        } else {
            for ( int i = 0; i < 42; i++ ) {
                if ( i < startDay || i - startDay >= totalDays ) {
                    days[ i ].UpdateColour ( Color.grey );
                } else {
                    days[ i ].UpdateColour ( Color.white );
                }

                days[ i ].UpdateDay ( i - startDay );
            }
        }

        if ( DateTime.Now.Year == year && DateTime.Now.Month == month ) {
            days[ ( DateTime.Now.Day - 1 ) + startDay ].UpdateColour ( Color.green );
        }

        UpdateCalendarWithUserData ( year, month, startDay );
    }

    private void UpdateCalendarWithUserData ( int year, int month, int startDay ) {
        for ( int i = 0; i < AppManager.instance._uData.emotionDates.Count; i++ ) {
            string[] splitDate = AppManager.instance._uData.emotionDates[ i ].Split ( '/' );

            if ( int.Parse ( splitDate[ 0 ] ) == year && int.Parse ( splitDate[ 1 ] ) == month ) {
                days[ int.Parse ( splitDate[ 2 ] ) + startDay - 1 ].UpdateColour ( Color.blue );
                days[ int.Parse ( splitDate[ 2 ] ) + startDay - 1 ].userDataIndex = i;
            }
        }
    }


    private int GetMonthStartDay ( int year, int month ) {
        DateTime temp = new DateTime ( year, month, 1 );

        return ( int ) temp.DayOfWeek;
    }

    private int GetTotalNumberOfDays ( int year, int month ) {
        return DateTime.DaysInMonth ( year, month );
    }

    public void SwitchMonth ( int direction ) {
        if ( direction < 0 ) {
            curDate = curDate.AddMonths ( -1 );
        } else {
            curDate = curDate.AddMonths ( 1 );
        }

        UpdateCalendar ( curDate.Year, curDate.Month );
    }

    private void OnDateButtonClicked ( Day day ) {
        checkInTexts[ 0 ].text = "Mood: ";
        checkInTexts[ 1 ].text = "Reason: ";

        if ( day.userDataIndex >= 0 ) {
            checkInTexts[ 0 ].text += moods[ AppManager.instance._uData.emotionValues[ day.userDataIndex ] ].ToString ();
            checkInTexts[ 1 ].text += contexts[ AppManager.instance._uData.contextValues[ day.userDataIndex ] ].ToString ();
        } else {
            checkInTexts[ 0 ].text += "No data found";
            checkInTexts[ 1 ].text += "No data found";
        }
    }

    private void ReadInTextData () {
        string moodPath;
        string moodFileData;

        string contextPath;
        string contextFileData;

#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
        moodPath = Path.Combine ( Application.streamingAssetsPath, "Moods.txt" );
        moodFileData = File.ReadAllText ( moodPath );

        contextPath = Path.Combine ( Application.streamingAssetsPath, "Contexts.txt" );
        contextFileData = File.ReadAllText ( contextPath );
#endif

#if UNITY_ANDROID
        moodPath = Path.Combine ( Application.streamingAssetsPath, "Moods.txt" );
        var loadingMoodRequest = UnityWebRequest.Get ( moodPath );
        loadingMoodRequest.SendWebRequest ();
        while ( !loadingMoodRequest.isDone && !loadingMoodRequest.isNetworkError && !loadingMoodRequest.isHttpError )
            ;
        moodFileData = System.Text.Encoding.UTF8.GetString ( loadingMoodRequest.downloadHandler.data );

        contextPath = Path.Combine ( Application.streamingAssetsPath, "Contexts.txt" );
        var loadingContextRequest = UnityWebRequest.Get ( contextPath );
        loadingContextRequest.SendWebRequest ();
        while ( !loadingContextRequest.isDone && !loadingContextRequest.isNetworkError && !loadingContextRequest.isHttpError )
            ;
        contextFileData = System.Text.Encoding.UTF8.GetString ( loadingContextRequest.downloadHandler.data );
#endif

        string[] splitMoodData = moodFileData.Split ( '\n' );
        string[] splitContextData = contextFileData.Split ( '\n' );

        foreach ( string line in splitMoodData ) {
            moods.Add ( line );
        }

        foreach ( string line in splitContextData ) {
            contexts.Add ( line );
        }
    }
}
