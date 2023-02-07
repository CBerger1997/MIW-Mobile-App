using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour {

    public class Day {
        public int dayNum;
        public Color dayColour;
        public GameObject obj;

        public Day(int dayNum, Color dayColour, GameObject obj) {
            this.dayNum = dayNum;
            this.dayColour = dayColour;
            this.obj = obj;
            obj.GetComponent<Image>().color = dayColour;
            UpdateDay(dayNum);
        }

        public void UpdateColour(Color newColor) {
            obj.GetComponent<Image>().color = newColor;
            dayColour = newColor;
        }

        public void UpdateDay(int newDayNum) {
            this.dayNum = newDayNum;

            if (dayColour == Color.white || dayColour == Color.green) {
                obj.GetComponentInChildren<TMP_Text>().text = (dayNum + 1).ToString();
            } else {
                obj.GetComponentInChildren<TMP_Text>().text = "";
            }
        }
    }

    private List<Day> days = new List<Day>();

    public Transform[] Weeks;

    public TMP_Text MonthAndYear;

    public DateTime curDate = DateTime.Now;

    private void Start() {
        UpdateCalendar(DateTime.Now.Year, DateTime.Now.Month);
    }

    private void UpdateCalendar(int year, int month) {
        DateTime temp = new DateTime(year, month, 1);
        curDate = temp;
        MonthAndYear.text = temp.ToString("MMMM") + " " + temp.Year.ToString();
        int startDay = GetMonthStartDay(year, month);
        int totalDays = GetTotalNumberOfDays(year, month);

        startDay--;

        if(startDay < 0) {
            startDay = 6;
        }

        if (days.Count == 0) {
            for (int w = 0; w < 6; w++) {
                for (int i = 0; i < 7; i++) {
                    Day newDay;
                    int curDay = (w * 7) + i;
                    if (curDay < startDay || curDay - startDay >= totalDays) {
                        newDay = new Day(curDay - startDay, Color.grey, Weeks[w].GetChild(i).gameObject);
                    } else {
                        newDay = new Day(curDay - startDay, Color.white, Weeks[w].GetChild(i).gameObject);
                    }
                    days.Add(newDay);
                }
            }
        } else {
            for (int i = 0; i < 42; i++) {
                if(i < startDay || i - startDay >= totalDays) {
                    days[i].UpdateColour(Color.grey);
                } else {
                    days[i].UpdateColour(Color.white);
                }

                days[i].UpdateDay(i - startDay);
            }
        }

        if(DateTime.Now.Year == year && DateTime.Now.Month == month) {
            days[(DateTime.Now.Day - 1) + startDay].UpdateColour(Color.green);
        }
    }


    int GetMonthStartDay(int year, int month) {
        DateTime temp = new DateTime(year, month, 1);

        return (int)temp.DayOfWeek;
    }

    private int GetTotalNumberOfDays(int year, int month) {
        return DateTime.DaysInMonth(year, month);
    }

    public void SwitchMonth(int direction) {
        if(direction < 0) {
            curDate = curDate.AddMonths(-1);
        } else {
            curDate = curDate.AddMonths(1);
        }

        UpdateCalendar(curDate.Year, curDate.Month);
    }

}
