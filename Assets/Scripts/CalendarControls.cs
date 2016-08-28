using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class CalendarControls : MonoBehaviour {

    public EventsCalendarMain obj_main;
    public GameObject Calendar;
    public EventHandler currentEventHandler;
    public Text MonthTitle;
    public GameObject[] CalendarDays;
    public GameObject EventTemplate;
    private DateTime currentDateTime;
    private Calendar currentCalendar;
    private int i_MonthValue = 0, i_YearValue = 0, i_DayValue = 0;
    private List<string[]> MyDatabase = new List<string[]>();
    private bool bMouseDown = false;

    // Use this for initialization
    void Start ()
    {
        //generate dummy data (will eventually fetch from file
        string[] dataEntry = { "0", "2016_8_2", "Random Event 1" };
        string[] dataEntry1 = { "1", "2016_8_28", "Random Event 2" };
        string[] dataEntry2 = { "2", "2016_8_14", "Random Event 3" };
        MyDatabase.Add(dataEntry);
        MyDatabase.Add(dataEntry1);
        MyDatabase.Add(dataEntry2);

        //get current date
        currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, new GregorianCalendar());
        currentCalendar = CultureInfo.InvariantCulture.Calendar;
        //set calendar to current date (month and such)
        SetCalendar(currentDateTime.Month, currentDateTime.Day, currentDateTime.Year);
        i_MonthValue = currentDateTime.Month;
        i_YearValue = currentDateTime.Year;
	}

    void OnEnable()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButtonDown(0) && !bMouseDown)
        {
            bMouseDown = true;
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count > 0)
            {
                foreach (RaycastResult r in raycastResults)
                {
                    Debug.Log("I Hit: " + r.gameObject.name.ToString());
                    if(r.gameObject.name == "Day BG")
                    {
                        //grab parent and fetch date
                        GameObject parent = r.gameObject.transform.parent.gameObject;
                        Text[] allSubTexts = parent.GetComponentsInChildren<Text>();
                        if(allSubTexts.Length > 0)
                        {
                            foreach(Text subText in allSubTexts)
                            {
                                if(subText.transform.name == "Day Number")
                                {
                                    i_DayValue = int.Parse(subText.text);
                                    Debug.Log("Day is: " + i_DayValue.ToString());
                                    obj_main.passThroughDate[0] = i_DayValue;
                                    obj_main.passThroughDate[1] = i_MonthValue;
                                    obj_main.passThroughDate[2] = i_YearValue;
                                    obj_main.isCalendarActive = false;
                                    obj_main.isChanged = true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Didn't hit anything");
            }
        }
        else
        {
            bMouseDown = false;
        }
	}

    void SetCalendar(int Month, int Day, int Year)
    {
        //check for active events and delete any that exist
        Text[] GrabEvents = Calendar.GetComponentsInChildren<Text>();
        foreach(Text sub in GrabEvents)
        {
            if(sub.name.StartsWith("Event Row "))
            {
                sub.text = " ";
            }
        }

        //Set Calendar Title Text
        string s_month = "";
        if      (Month == 1)        { s_month = "January"; }
        else if (Month == 2)        { s_month = "February"; }
        else if (Month == 3)        { s_month = "March"; }
        else if (Month == 4)        { s_month = "April"; }
        else if (Month == 5)        { s_month = "May"; }
        else if (Month == 6)        { s_month = "June"; }
        else if (Month == 7)        { s_month = "July"; }
        else if (Month == 8)        { s_month = "August"; }
        else if (Month == 9)        { s_month = "September"; }
        else if (Month == 10)       { s_month = "October"; }
        else if (Month == 11)       { s_month = "November"; }
        else if (Month == 12)       { s_month = "December"; }
        else { Debug.LogError("Invalid Month"); }
        MonthTitle.text = s_month + " " + Year.ToString();

        //get first day of current month
        DateTime firstOfMonth = new DateTime(Year, Month, 1);
        DayOfWeek firstDayOfMonth = currentCalendar.GetDayOfWeek(firstOfMonth);
        int startDay = 0, dateToSet = 1, maxMonthDays = DateTime.DaysInMonth(Year, Month);
        bool firstDaySet = false;
        if(firstDayOfMonth == DayOfWeek.Sunday) { startDay = 0; }
        else if(firstDayOfMonth == DayOfWeek.Monday) { startDay = 1; }
        else if (firstDayOfMonth == DayOfWeek.Tuesday) { startDay = 2; }
        else if (firstDayOfMonth == DayOfWeek.Wednesday) { startDay = 3; }
        else if (firstDayOfMonth == DayOfWeek.Thursday) { startDay = 4; }
        else if (firstDayOfMonth == DayOfWeek.Friday) { startDay = 5; }
        else if (firstDayOfMonth == DayOfWeek.Saturday) { startDay = 6; }

        for(int i = 0; i < CalendarDays.Length; i++)
        {
            if (!firstDaySet)
            {
                if (i == startDay)
                {
                    CalendarDays[i].SetActive(true);
                    Transform[] subTransforms = CalendarDays[i].GetComponentsInChildren<Transform>();
                    foreach (Transform t in subTransforms)
                    {
                        if (t.name == "Events")
                        {
                            string compareDate = Year.ToString() + "_" + Month.ToString() + "_" + dateToSet.ToString();
                            int numShown = 0;
                            foreach (string[] s in MyDatabase)
                            {
                                if (numShown < 4)
                                {
                                    if (s[1] == compareDate)
                                    {
                                        numShown++;
                                        Transform[] t2 = t.GetComponentsInChildren<Transform>();
                                        foreach(Transform t3 in t2)
                                        {
                                            if(t3.name == "Event Row " + numShown.ToString())
                                            {
                                                t3.GetComponent<Text>().text = s[2];
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (t.name == "Day Number")
                        {
                            t.GetComponent<Text>().text = dateToSet.ToString();
                            dateToSet++;
                            firstDaySet = true;
                        }
                    }
                }
                else
                {
                    CalendarDays[i].SetActive(false);
                }
            }
            else
            {
                if(dateToSet <= maxMonthDays)
                {
                    CalendarDays[i].SetActive(true);
                    Transform[] subTransforms = CalendarDays[i].GetComponentsInChildren<Transform>();
                    foreach (Transform t in subTransforms)
                    {
                        if (t.name == "Events")
                        {
                            string compareDate = Year.ToString() + "_" + Month.ToString() + "_" + dateToSet.ToString();
                            int numShown = 0;
                            foreach (string[] s in MyDatabase)
                            {
                                if (numShown < 4)
                                {
                                    if (s[1] == compareDate)
                                    {
                                        numShown++;
                                        Transform[] t2 = t.GetComponentsInChildren<Transform>();
                                        foreach (Transform t3 in t2)
                                        {
                                            if (t3.name == "Event Row " + numShown.ToString())
                                            {
                                                t3.GetComponent<Text>().text = s[2];
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (t.name == "Day Number")
                        {
                            t.GetComponent<Text>().text = dateToSet.ToString();
                            dateToSet++;
                        }
                    }
                }
                else
                {
                    CalendarDays[i].SetActive(false);
                }
            }
        }
    }

    public void OnBackClick()
    {
        i_MonthValue--;
        if (i_MonthValue < 1)
        {
            i_MonthValue = 12;
            i_YearValue--;
        }
        SetCalendar(i_MonthValue, 1, i_YearValue);
    }

    public void OnNextClick()
    {
        i_MonthValue++;
        if(i_MonthValue > 12)
        {
            i_MonthValue = 1;
            i_YearValue++;
        }
        SetCalendar(i_MonthValue, 1, i_YearValue);
    }

    public void OnAddNewClick()
    {

    }

    public void OnDayClick()
    {
        Debug.Log("Day Clicked");
    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("Main");
    }
}
