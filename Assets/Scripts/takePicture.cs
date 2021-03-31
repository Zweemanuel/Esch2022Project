using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takePicture : MonoBehaviour
{

    private bool capturing = false;
    private int captureDelay = 0;

    // First hide the canvas with the buttons, then take picture and show the buttons again
    public void photo()
    {
        GameObject.Find("buttonCanvas").GetComponent<Canvas>().enabled = false;
        capturing = true;
    }

    // Adding a slight delay when removing the buttons and taking the picture
    void Update()
    {
        if (capturing == true)
        {
            captureDelay++;
        }
        if (captureDelay > 25)
        {
            StartCoroutine("CaptureScreen");
            capturing = false;
            captureDelay = 0;
        }
    }

    // Capture the screen and save the picture in the internal memory
    IEnumerator CaptureScreen()
    {
        string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "Esch_Picture" + timeStamp + ".png";
        string pathToSave = fileName;

        yield return new WaitForEndOfFrame();

        ScreenCapture.CaptureScreenshot(pathToSave);
        GameObject.Find("buttonCanvas").GetComponent<Canvas>().enabled = true;



    }
}
