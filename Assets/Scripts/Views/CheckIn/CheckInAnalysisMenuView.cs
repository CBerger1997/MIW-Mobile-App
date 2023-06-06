using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using static CheckInAnalysisMenuView;

public class CheckInAnalysisMenuView : View, IDataPersistence
{
    public class Day
    {
        public int dayNum;
        public Color dayColour;
        public GameObject obj;
        public List<int> userDataIndex;

        public Day ( int dayNum, Color dayColour, GameObject obj )
        {
            this.dayNum = dayNum;
            this.dayColour = dayColour;
            this.obj = obj;
            this.userDataIndex = new List<int> ();
            obj.GetComponent<Image> ().color = dayColour;
            UpdateDay ( dayNum );
        }

        public void UpdateColour ( Color newColor )
        {
            obj.GetComponent<Image> ().color = newColor;
            dayColour = newColor;
        }

        public void UpdateDay ( int newDayNum )
        {
            this.dayNum = newDayNum;

            if ( obj.GetComponent<Button>().IsInteractable() )
            {
                obj.GetComponentInChildren<TMP_Text> ().text = ( dayNum + 1 ).ToString ();
            }
            else
            {
                obj.GetComponentInChildren<TMP_Text> ().text = "";
            }
        }
    }

    [SerializeField] private Transform[] Weeks;
    [SerializeField] private TMP_Text MonthAndYear;
    [SerializeField] private GameObject CheckInTextPanel;
    [SerializeField] private GameObject _toggleContent;
    [SerializeField] private GameObject _checkInValuePrefab;

    private DateTime curDate = DateTime.Now;
    private List<Day> days = new List<Day> ();

    private List<TMP_Text> checkInTexts = new List<TMP_Text> ();

    private List<string> moods = new List<string> ();
    private List<string> contexts = new List<string> ();

    private List<string> emotionDates = new List<string> ();
    private List<int> emotionVals = new List<int> ();
    private List<string> reasonVals = new List<string> ();

    private int _currentOptionSelection;
    private Day _selectedDay;

    public override void Initialise ()
    {
        _toggleContent.GetComponent<ScrollSwipe> ().OnSelectionChange += OnSelectionChangeHandler;

        UpdateCalendar ( DateTime.Now.Year, DateTime.Now.Month );

        foreach ( TMP_Text text in CheckInTextPanel.GetComponentsInChildren<TMP_Text> () )
        {
            checkInTexts.Add ( text );
        }

        foreach ( Day day in days )
        {
            day.obj.GetComponent<Button> ().onClick.AddListener ( delegate { OnDateButtonClicked ( day ); } );
        }

        ReadInTextData ();
    }

    public override void Show ()
    {
        base.Show ();

        UpdateCalendar ( DateTime.Now.Year, DateTime.Now.Month );
    }

    private void UpdateCalendar ( int year, int month )
    {
        DateTime temp = new DateTime ( year, month, 1 );
        curDate = temp;
        MonthAndYear.text = temp.ToString ( "MMMM" ) + " " + temp.Year.ToString ();
        int startDay = GetMonthStartDay ( year, month );
        int totalDays = GetTotalNumberOfDays ( year, month );

        startDay--;

        if ( startDay < 0 )
        {
            startDay = 6;
        }

        if ( days.Count == 0 )
        {
            for ( int w = 0; w < 6; w++ )
            {
                for ( int i = 0; i < 7; i++ )
                {
                    Day newDay;
                    int curDay = ( w * 7 ) + i;
                    if ( curDay < startDay || curDay - startDay >= totalDays )
                    {
                        newDay = new Day ( curDay - startDay, Color.grey, Weeks[ w ].GetChild ( i ).gameObject );
                    }
                    else
                    {
                        newDay = new Day ( curDay - startDay, Color.white, Weeks[ w ].GetChild ( i ).gameObject );
                    }
                    days.Add ( newDay );
                }
            }
        }
        else
        {
            for ( int i = 0; i < 42; i++ )
            {
                if ( i < startDay || i - startDay >= totalDays )
                {
                    days[ i ].UpdateColour ( Color.white );
                    days[ i ].obj.GetComponent<Button> ().interactable = false;
                    days[ i ].obj.GetComponent<Image> ().enabled = false;
                } 
                else
                {
                    days[ i ].UpdateColour ( Color.white );
                    days[ i ].obj.GetComponent<Button> ().interactable = true;
                    days[ i ].obj.GetComponent<Image> ().enabled = true;
                }

                days[ i ].UpdateDay ( i - startDay );
                days[ i ].userDataIndex.Clear ();
            }
        }

        if ( DateTime.Now.Year == year && DateTime.Now.Month == month )
        {
            days[ ( DateTime.Now.Day - 1 ) + startDay ].UpdateColour ( Color.green );
        }

        UpdateCalendarWithUserData ( year, month, startDay );
    }

    private void UpdateCalendarWithUserData ( int year, int month, int startDay )
    {
        for ( int i = 0; i < emotionDates.Count; i++ )
        {
            string[] splitDate = emotionDates[ i ].Split ( '/' );

            if ( int.Parse ( splitDate[ 0 ] ) == year && int.Parse ( splitDate[ 1 ] ) == month ) {

                Color blue;

                if ( ColorUtility.TryParseHtmlString ( "#8bd2eb", out blue ) ) {
                    days[ int.Parse ( splitDate[ 2 ] ) + startDay - 1 ].UpdateColour ( blue );
                }
                    
                days[ int.Parse ( splitDate[ 2 ] ) + startDay - 1 ].userDataIndex.Add ( i );
            }
        }
    }

    private int GetMonthStartDay ( int year, int month )
    {
        DateTime temp = new DateTime ( year, month, 1 );

        return ( int ) temp.DayOfWeek;
    }

    private int GetTotalNumberOfDays ( int year, int month )
    {
        return DateTime.DaysInMonth ( year, month );
    }

    public void SwitchMonth ( int direction )
    {
        if ( direction < 0 )
        {
            curDate = curDate.AddMonths ( -1 );
        }
        else
        {
            curDate = curDate.AddMonths ( 1 );
        }

        UpdateCalendar ( curDate.Year, curDate.Month );
    }

    private void OnDateButtonClicked ( Day day )
    {
        //Sets the selected day to the selected day variable
        _selectedDay = day;

        //Destroys all existing children of the scroll swipe content
        foreach ( Transform child in _toggleContent.transform )
        {
            Destroy ( child.gameObject );
        }

        //Presets the text for mood and reason
        checkInTexts[ 0 ].text = "Mood: ";
        checkInTexts[ 1 ].text = "Reason: ";

        //Checks the selected day contains data for mood and reason
        if ( day.userDataIndex.Count > 0 )
        {
            //Instantiates text for each available user data for the selected day
            for ( int i = 0; i < day.userDataIndex.Count; i++ )
            {
                GameObject newScrollText = Instantiate ( _checkInValuePrefab, _toggleContent.transform );
                newScrollText.GetComponent<TMP_Text> ().text = ( i + 1 ).ToString ();
            }

            //Recalculates the object positions for the scroll swipe
            _toggleContent.GetComponent<ScrollSwipe> ().ReinitialiseScrollPositions ( day.userDataIndex.Count );

            //Presets the scroll swipe position to the first position
            _toggleContent.GetComponent<ScrollSwipe> ().PresetPosition ( 0 );

            //Reset the current option selection to 0
            _currentOptionSelection = 0;

            //Sets the text for the mood chosen on the selected day
            checkInTexts[ 0 ].text += moods[ emotionVals[ day.userDataIndex[ _currentOptionSelection ] ] ].ToString ();

            //Splits the string of the saved reasons for the selected day
            string[] reasons = reasonVals[ day.userDataIndex[ _currentOptionSelection ] ].Split ( ':' );

            //Adds each reason to the reasons text to show all reasons selected on the selected day
            for ( int i = 0; i < reasons.Length; i++ )
            {
                checkInTexts[ 1 ].text += contexts[ int.Parse ( reasons[ i ] ) ].ToString ();

                if ( reasons.Length > 0 && i < reasons.Length - 1 )
                {
                    checkInTexts[ 1 ].text += ", ";
                }
            }
            //Remove overlapping text by removing the carriage return character
            checkInTexts[ 1 ].text = checkInTexts[ 1 ].text.Replace ( "\r", "" );
        }
        else
        {
            //Sets the mood and reason texts to no data found if the date contains no past data
            checkInTexts[ 0 ].text += "No data found";
            checkInTexts[ 1 ].text += "No data found";
        }
    }

    private void OnSelectionChangeHandler ()
    {
        //Checks that the toggle contains children
        if ( _toggleContent.transform.childCount > 0 )
        {
            //Sets val to the current selection of the toggle content
            int val = _toggleContent.GetComponent<ScrollSwipe> ().selection;

            //Checks if the new selection is different from the previous selection
            if ( val != _currentOptionSelection )
            {
                //Sets the current selection to the new selection
                _currentOptionSelection = val;

                //Presets the text for mood and reason
                checkInTexts[ 0 ].text = "Mood: ";
                checkInTexts[ 1 ].text = "Reason: ";

                //Sets the text for the mood chosen on the selected day
                checkInTexts[ 0 ].text += moods[ emotionVals[ _selectedDay.userDataIndex[ _currentOptionSelection ] ] ].ToString ();

                //Splits the string of the saved reasons for the selected day
                string[] reasons = reasonVals[ _selectedDay.userDataIndex[ _currentOptionSelection ] ].Split ( ':' );

                //Adds each reason to the reasons text to show all reasons selected on the selected day
                for ( int i = 0; i < reasons.Length; i++ )
                {
                    checkInTexts[ 1 ].text += contexts[ int.Parse ( reasons[ i ] ) ].ToString ();

                    if ( reasons.Length > 0 && i < reasons.Length - 1 )
                    {
                        checkInTexts[ 1 ].text += ", ";
                    }
                }
                //Remove overlapping text by removing the carriage return character
                checkInTexts[ 1 ].text = checkInTexts[ 1 ].text.Replace ( "\r", "" );
            }
        }
    }

    private void ReadInTextData ()
    {
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

        foreach ( string line in splitMoodData )
        {
            moods.Add ( line );
        }

        foreach ( string line in splitContextData )
        {
            contexts.Add ( line );
        }
    }

    public void LoadData ( UserData data )
    {
        emotionDates = data.emotionDates;
        emotionVals = data.emotionValues;
        reasonVals = data.reasonValues;
    }

    public void SaveData ( ref UserData data )
    {

    }
}
