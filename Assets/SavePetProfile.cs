using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
using System.Diagnostics;
using System;
using System.IO;
public class SavePetProfile : MonoBehaviour {

	public Text name_field, age_field, about_me, status;
	public GameObject path;
	//then drag and drop the Username_field



	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void PetsOnSaveClick()
	{
		UnityEngine.Debug.Log ("Saving");
		string name = name_field.text.ToString();
		string age = age_field.text.ToString();
		string aboutme = about_me.text.ToString();
		string stat = status.text.ToString();
		try 
		{

			//Pass the filepath and filename to the StreamWriter Constructor
			StreamWriter swt = new StreamWriter("C:\\Users\\micah\\Documents\\p4pMicah\\Assets\\PetProfile.txt");
			StreamReader sr = new StreamReader("C:\\Users\\micah\\Documents\\p4pMicah\\Assets\\IDs.txt");
			//Write a line of text
			String id = sr.ReadLine();
			sr.Close();
			int x = int.Parse(id);
			x++;
			String idx = x.ToString();
			File.WriteAllText ("C:\\Users\\micah\\Documents\\p4pMicah\\Assets\\IDs.txt", idx);
			swt.Write(x+" - ");
			swt.Write("PetName="+name+",");
			swt.Write("PetAge="+age+",");
			swt.Write("AboutPet="+aboutme+",");
			swt.Write("StatusofPet="+stat);
			swt.Close();
			UnityEngine.Debug.Log("Files Updated: "+name+", "+idx+", "+age+", "+aboutme+", "+stat);

		}

		catch(Exception e)
		{
			UnityEngine.Debug.Log("Exception: " + e.Message);
		}
		finally 
		{
		}
	}
}
