using UnityEngine;

public class EventsCalendarMain : MonoBehaviour
{
    public GameObject CalendarView, EventView;
    public bool isCalendarActive = true, isChanged = false;
    public int[] passThroughDate = { 0, 0, 0 }; //day, month, year

	void Awake ()
    {
        CalendarView.SetActive(true);
        EventView.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isChanged)
        {
            if(isCalendarActive)
            {
                CalendarView.SetActive(true);
                EventView.SetActive(false);
            }
            else
            {
                CalendarView.SetActive(false);
                EventView.SetActive(true);
            }
            isChanged = false;
        }
	}
}
