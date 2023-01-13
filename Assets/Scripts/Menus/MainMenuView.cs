using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuView : View {
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _crisisButton;
    [SerializeField] private Button _affirmationButton;
    [SerializeField] private Button _visualisationsButton;
    [SerializeField] private Button _justBreathButton;
    [SerializeField] private Button _journalButton;
    [SerializeField] private Button _checkInButton;
    [SerializeField] private Button _checkInAnalysisButton;
    [SerializeField] private GameObject _calendarContent;
    [SerializeField] private Scrollbar _calendarScrollbar;

    private GameObject _dateButton;

    public override void Initialise() {
        _settingsButton.onClick.AddListener(() => ViewManager.Show<SettingsMenuView>());

        _crisisButton.onClick.AddListener(() => ViewManager.Show<CrisisMenuView>());

        _affirmationButton.onClick.AddListener(() => ViewManager.Show<AffirmationMenuView>());

        _visualisationsButton.onClick.AddListener(() => ViewManager.Show<VisualisationsMenuView>());

        _justBreathButton.onClick.AddListener(() => ViewManager.Show<JustBreathMenuView>());

        _journalButton.onClick.AddListener(() => ViewManager.Show<JournalMenuView>());

        _checkInButton.onClick.AddListener(() => ViewManager.Show<CheckInMenuView>());

        _checkInAnalysisButton.onClick.AddListener(() => ViewManager.Show<CheckInAnalysisMenuView>());

        _dateButton = (GameObject)Resources.Load("Prefab/Menu/Date Button (TMP)");

        CreateCalendarScrollView();
    }

    public override void Show() {
        base.Show();

        if (AppManager.instance._uData.hasUserCheckedIn) {
            _checkInButton.GetComponent<Image>().color = Color.green;
            _checkInButton.enabled = false;
        }

        _calendarScrollbar.value = 1;
    }

    private void CreateCalendarScrollView() {
        int curDay = System.DateTime.Now.Day;
        int curMonth = System.DateTime.Now.Month;
        int curYear = System.DateTime.Now.Year;

        int startDay = 1;
        int startMonth = curMonth - 1;
        int startYear;

        if (startMonth == 0) {
            startMonth = 12;
            startYear = curYear - 1;
        } else {
            startYear = curYear;
        }

        int startDaysInMonth = DaysInMonth(startMonth, startYear);

        for (int i = 0; i < (startDaysInMonth + curDay); i++) {
            GameObject newDate = Instantiate(_dateButton, _calendarContent.transform);
            newDate.transform.GetChild(0).GetComponent<TMP_Text>().text = ConvertMonthToString(startMonth);
            newDate.transform.GetChild(1).GetComponent<TMP_Text>().text = startDay.ToString();
            startDay++;

            if (startDay > startDaysInMonth) {
                startDay = 1;
                startMonth = curMonth;
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
                return "Jan";
            case 2:
                return "Feb";
            case 3:
                return "Mar";
            case 4:
                return "Apr";
            case 5:
                return "May";
            case 6:
                return "Jun";
            case 7:
                return "Jul";
            case 8:
                return "Aug";
            case 9:
                return "Sep";
            case 10:
                return "Oct";
            case 11:
                return "Nov";
            case 12:
                return "Dec";
            default:
                return "";
        }
    }
}