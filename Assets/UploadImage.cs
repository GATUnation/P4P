using UnityEngine;
using System.Collections;
using System.Diagnostics;
public class UploadImage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UploadOnClick()
	{
		Process.Start ("explorer.exe", "-p");
	}
}
