using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System;
using System.IO;
using System.Collections;
using UnityEngine.Sprites;
using UnityEngine.UI;
public class ImageUpload : MonoBehaviour
{

    public string path;
    private bool windowOpen;
	//public GameObject profilepic;

    public void UploadOnClick()
    {
        //Process.Start ("explorer.exe", "-p");
        //OpenFileDialog filedialog1 = new OpenFileDialog();
        //FileSelector.GetFile(SelectFileFunction callback, string extension)
        Process.Start("C:\\Users\\micah\\Documents\\p4pMicah\\OpenFileDialog3.jar");
        //C:\Users\micah\Documents\p4pMicah 
		findDir();
    }

    public void findDir()
    {
		//profilepic.GetComponent<Image>().sprite = Resources.Load<Sprite>("MySprite.jpg");

        bool notfound = true;
		String path =("C:\\Users\\micah\\Documents\\p4pMicah\\Assets\\DonorStatusDir.txt");
		File.WriteAllText (path, String.Empty);
        while (notfound==true)
        {
            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:\\Users\\micah\\Documents\\p4pMicah\\Assets\\DonorStatusDir.txt");

                //Read the first line of text
                line = sr.ReadLine();

                //Continue to read until you reach end of file
                if (line != null)
                {
                    //write the lie to console window
					UnityEngine.Debug.Log(line);
                    //Read the next line
                    line = sr.ReadLine();
					notfound = false;

                }
				if(line==null)
				{
				}
                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
				UnityEngine.Debug.Log (e);
            }
            finally
            {
            }
        }
    }
}
