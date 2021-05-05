using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionScript : MonoBehaviour
{
    [SerializeField] private Canvas introCanvas;
    [SerializeField] private Text currentIntroText;
    [SerializeField] private List<System.String> introText;
    [SerializeField] private List<Image> introImg;
    private int introSize = 0;

    private bool introSequence = true;
    // Start is called before the first frame update
    public void Start()
    {
        introCanvas.enabled = false ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&(introCanvas.enabled==true))
        {
            switchNext();
        }
    }

    public void startIntro()
    {
        introCanvas.enabled = true;
        switchHighlight(99);
    }
    private void switchHighlight(int n)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == n)
            {
                introImg[i].enabled = true;
            }
            else
            {
                introImg[i].enabled = false;
            }
            
        }
    }
    public void switchNext()
    {
        if (introSize < 5)
        {
            switchHighlight(introSize);
            currentIntroText.text = introText[introSize];
            introSize++;
        }
        else
        {
            switchHighlight(99);
            introCanvas.enabled = false;
            introSize = 0;
            currentIntroText.text = introText[5];
        }
        
    }
}
