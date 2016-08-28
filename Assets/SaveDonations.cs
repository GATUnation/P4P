using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
using System.Collections;
using UnityEngine.Sprites;
public class SaveDonations : MonoBehaviour {

	public Text name_field, donations_field, contact_info, company, anonymous;
	public GameObject path;
	//then drag and drop the Username_field



	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnSaveClick()
	{
		
		string userID = name_field.text.ToString();
		string donations = donations_field.text.ToString();
		string contact = contact_info.text.ToString();
		string thecompany = company.text.ToString();
		string ifanonomous = anonymous.text.ToString();
		try 
		{

			//Pass the filepath and filename to the StreamWriter Constructor
			StreamWriter sw = new StreamWriter("C:\\Users\\micah\\Documents\\p4pMicah\\Assets\\DonorStatus.txt");
			StreamReader sr = new StreamReader("C:\\Users\\micah\\Documents\\p4pMicah\\Assets\\IDs.txt");
			//Write a line of text
			String id = sr.ReadLine();
			sr.Close();
			int x = int.Parse(id);
			x++;
			String idx = x.ToString();
			File.WriteAllText ("C:\\Users\\micah\\Documents\\p4pMicah\\Assets\\IDs.txt", idx);
			sw.Write(idx+" - ");
			sw.Write("DonorName="+userID+",");
			sw.Write("MoneyDonated="+donations+",");
			sw.Write("DonorContactInfo="+contact+",");
			sw.Write("DonorCompany="+thecompany+",");
			sw.Write("DonorAnonomous="+ifanonomous+",");
			sw.Close();
			UnityEngine.Debug.Log("Files Updated: "+userID+", "+donations+", "+contact+", "+thecompany+", "+ifanonomous);

			GetComponent<Image>().sprite = Resources.Load<Sprite>("MySprite");
        }

		catch(Exception e)
		{
			Console.WriteLine("Exception: " + e.Message);
		}
		finally 
		{
			Console.WriteLine("Executing finally block.");
		}
	}
}
