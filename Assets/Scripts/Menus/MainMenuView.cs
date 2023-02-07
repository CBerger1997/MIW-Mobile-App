using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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

        _dateButton = (GameObject)Resources.Load("Prefab/Menu Specific/Date Button (TMP)");

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
        DateTime curDate = DateTime.Now.AddMonths(-1);

        int startDay = 1;

        int startDaysInMonth = GetTotalNumberOfDays(curDate.Year, curDate.Month);

        for (int i = 0; i < (startDaysInMonth + DateTime.Now.Day); i++) {
            GameObject newDate = Instantiate(_dateButton, _calendarContent.transform);
            newDate.transform.GetChild(0).GetComponent<TMP_Text>().text = curDate.ToString("MMM");
            newDate.transform.GetChild(1).GetComponent<TMP_Text>().text = startDay.ToString();
            startDay++;

            if (startDay > startDaysInMonth) {
                startDay = 1;
                curDate = DateTime.Now;
            }
        }
    }

    private int GetTotalNumberOfDays(int year, int month) {
        return DateTime.DaysInMonth(year, month);
    }
}