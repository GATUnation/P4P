using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuForce : MonoBehaviour {

	public void OnMainMenuForceClick()
    {
        SceneManager.LoadScene("Main");
    }
}
