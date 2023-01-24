using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckInAnalysisMenuView : View {
    [SerializeField] private Button _backButton;
    [SerializeField] private GameObject _calendarContentParent;

    private GameObject _calendarButton;

    public override void Initialise() {
        _backButton.onClick.AddListener(() => ViewManager.ShowLast());

        _calendarButton = (GameObject)Resources.Load("Prefab/Specific/Calendar Button");

        GenerateCalendarView();
    }

    private void GenerateCalendarView() {
        int width = 700;
        int height = 500;

        int x = 7;

        int curDay = System.DateTime.Now.Day;
        int curMonth = 2;//System.DateTime.Now.Month;
        int curYear = System.DateTime.Now.Year;

        int y;

        int daysInMonth = DaysInMonth(curMonth, curYear);

        if (daysInMonth % 7 != 0) {
            y = 5;
        } else {
            y = 4;
        }

        int buttonWidth = width / x;
        int buttongHeight = height / y;

        Debug.Log(buttonWidth);
        Debug.Log(buttongHeight);

        int dateCounter = 1;

        for (int iy = 0; iy < y; iy++) {
            for (int ix = 0; ix < x; ix++) {
                GameObject newButton = Instantiate(_calendarButton, _calendarContentParent.transform);
                newButton.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth, buttongHeight);
                newButton.GetComponent<RectTransform>().localPosition = new Vector3(ix * buttonWidth + (buttonWidth/2), -(iy * buttongHeight + buttongHeight/2) - 100, 1);
                newButton.transform.GetChild(0).GetComponent<TMP_Text>().text = dateCounter.ToString();

                dateCounter++;

                if(dateCounter > daysInMonth) {
                    return;
                }
            }
        }
    }

    private int DaysInMonth(int month, int year) {
        if (month == 4 || month == 6 || month == 9 || month == 11) {
            return 30;
        } else if (month == 2) {
            if (year % 4 == 0) {
                return 29;
            } else {
                return 28;
            }
        } else {
            return 31;
        }
    }

    private string ConvertMonthToString(int month) {
        switch (month) {
            case 1:
                return "January";
            case 2:
                return "February";
            case 3:
                return "March";
            case 4:
                return "April";
            case 5:
                return "May";
            case 6:
                return "June";
            case 7:
                return "July";
            case 8:
                return "August";
            case 9:
                return "September";
            case 10:
                return "October";
            case 11:
                return "November";
            case 12:
                return "December";
            default:
                return "";
        }
    }

}
