using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAudio : MonoBehaviour
{
    private bool toggle = true;
    public Text mytext;

    // Update is called once per frame
    void Update()
    {
        if (AudioListener.volume == 1f)
        {
            mytext.text = "On";
        }
        else if (AudioListener.volume == 0f)
        {
            mytext.text = "Off";
        }
    }

    public void toggleSound()
    {
        toggle = !toggle;
        if (toggle)
        {
            AudioListener.volume = 1f;
        } else
        {
            AudioListener.volume = 0f;
        }
    }
}
