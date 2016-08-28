using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigation : MonoBehaviour
{
    public void OnProfilesClick()
    {
        SceneManager.LoadScene("PetProfilesSearch");
    }

    public void OnEventsClick()
    {
        SceneManager.LoadScene("EventsCalendar");
    }

    public void OnDontationsClick()
    {
        SceneManager.LoadScene("DonationsSearch");
    }

    public void OnFormLettersClick()
    {
        SceneManager.LoadScene("FormLettersProfiles");
    }
}
