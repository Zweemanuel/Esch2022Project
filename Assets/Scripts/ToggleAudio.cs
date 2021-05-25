using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAudio : MonoBehaviour
{
    private bool toggle = true;
    public void ChangeTextOnClick(Text textfield)
    {
        if (textfield.text == "On" )
        {
            textfield.text = "Off";
        } else if (textfield.text == "Off" )
        {
            textfield.text = "On";
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
