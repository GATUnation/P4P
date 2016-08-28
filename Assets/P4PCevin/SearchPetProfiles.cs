using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class SearchPetProfiles : MonoBehaviour {

    public GameObject Content;
    public GameObject prof;
    public Text SearchText;

    private Dictionary<int, Dictionary<string, string>> petProfiles;
    // Use this for initialization
    void Start () {
        
	}

    public void clearAll()
    {
        petProfiles = new Dictionary<int, Dictionary<string, string>>();
        Transform[] thing = Content.GetComponentsInChildren<Transform>();
        foreach (Transform t in thing)
        {
            if (t.name != "Content")
            {
                Destroy(t.gameObject);
            }
        }
        recreate();
    }
    public void recreate()
    {
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
            foreach (string data in profiledata)
            {
                string[] keyvalue = data.Split(del);
                profile.Add(keyvalue[0], keyvalue[1]);
            }
            petProfiles.Add(index, profile);
        }
        sr.Close();
        CreateInstances();
    }

    public void CreateInstances()
    {
        int ypos = 0;
        for (int i = 0; i < petProfiles.Count; i++)
        {
            Dictionary<string, string> profile = petProfiles[i];
            if (profile["PetName"].ToUpper().Contains(SearchText.text.ToUpper())
                || profile["AboutPet"].ToUpper().Contains(SearchText.text.ToUpper()) 
                || profile["StatusofPet"].ToUpper().Contains(SearchText.text.ToUpper())
                || SearchText.text == "")
            {
                ypos++;
                GameObject prefabInstance = UnityEngine.Object.Instantiate(prof);
                prefabInstance.transform.SetParent(Content.transform);
                prefabInstance.transform.localPosition = new Vector3(0, -56f * (ypos), 0);
                prefabInstance.transform.localScale = new Vector3(1, 1, 1);

                Transform[] texts = prefabInstance.GetComponentsInChildren<Transform>();

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
                if (Content.GetComponent<RectTransform>().sizeDelta.y < (ypos) * 56f)
                {
                    Content.GetComponent<RectTransform>().sizeDelta = new Vector2(Content.GetComponent<RectTransform>().sizeDelta.x, (i + 1) * 56f);
                }
            }
        }
        //button.onClick.AddListener(() => { OnButtonClicked(i); });
    }

// Update is called once per frame
void Update () {
	    
	}
}
