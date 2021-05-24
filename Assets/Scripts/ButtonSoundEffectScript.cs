using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundEffectScript : MonoBehaviour
{
    private static ButtonSoundEffectScript instance;
    
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
