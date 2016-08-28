using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class LoadPetProfiles : MonoBehaviour {
    
    public GameObject prof;
    public Text profText3;
    public GameObject content;

    private Dictionary<int, Dictionary<string, string>> petProfiles = new Dictionary<int, Dictionary<string, string>>();

	// Use this for initialization
	void Start () {
        StreamReader sr = new StreamReader("assets/PetProfile.txt");
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            Char delimiters = '-';
            int index = Convert.ToInt32(line.Split(delimiters)[0].Trim());
            Char delimiter = ',';
            Char del = '=';
            Dictionary<string, string> profile = new Dictionary<string, string>();
            string[] profiledata = line.Split(delimiters)[1].Trim().Split(delimiter);
            foreach(string data in profiledata)
            {
                string[] keyvalue = data.Split(del);
                profile.Add(keyvalue[0], keyvalue[1]);
            }
            petProfiles.Add(index, profile);
        }
        CreateInstances();
    }

    public void CreateInstances()
    {
        int ypos = 0;
        for (int i = 0; i < petProfiles.Count; i++)
        {
            ypos++;
            Dictionary<string, string> profile = petProfiles[i];
            GameObject prefabInstance = UnityEngine.Object.Instantiate(prof);
            prefabInstance.transform.SetParent(content.transform);
            prefabInstance.transform.localPosition = new Vector3(0, -56f * (ypos), 0);
            prefabInstance.transform.localScale = new Vector3(1, 1, 1);

            Transform[] texts = prefabInstance.GetComponentsInChildren<Transform>();
            profText3.text = texts.Length.ToString();
            for (int j = 0; j < texts.Length; j++)
            {
                if (texts[j].name == "PetName")
                {
                    texts[j].GetComponent<Text>().text = profile["PetName"];
                }
                else if (texts[j].name == "PetAge")
                {
                    texts[j].GetComponent<Text>().text = profile["PetAge"];
                }
                else if (texts[j].name == "AboutPet")
                {
                    texts[j].GetComponent<Text>().text = profile["AboutPet"];
                }
                else if (texts[j].name == "StatusofPet")
                {
                    texts[j].GetComponent<Text>().text = profile["StatusofPet"];
                }

            }
            profText3.text = petProfiles.Count.ToString();
            if (content.GetComponent<RectTransform>().sizeDelta.y < ypos * 56f)
            {
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, ypos * 56f);
            }
        }
        //button.onClick.AddListener(() => { OnButtonClicked(i); });
    }

    // Update is called once per frame
    void Update () {
        
	}
}
