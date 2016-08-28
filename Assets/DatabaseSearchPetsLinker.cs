using UnityEngine;
using UnityEngine.SceneManagement;

public class DatabaseSearchPetsLinker : MonoBehaviour
{
    public void OnPetProfileClick()
    {
        SceneManager.LoadScene("PetProfiles");
    }
}
