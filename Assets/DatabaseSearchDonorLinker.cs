using UnityEngine;
using UnityEngine.SceneManagement;

public class DatabaseSearchDonorLinker : MonoBehaviour
{
    public void OnDonorProfileClick()
    {
        SceneManager.LoadScene("DonationProfile");
    }
}
