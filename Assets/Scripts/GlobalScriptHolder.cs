using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalScriptHolder : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main");
        }
	}
}
