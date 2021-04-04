using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private Quizmanager quizManager;
    [SerializeField] private Text questionText;
    [SerializeField] private Image questionImage;
    [SerializeField] private List<Button> options;
    [SerializeField] private Color correctColor, wrongColor, neutralColor;
    [SerializeField] private int maxQuestionsPerGame;
    private Question question;

    private bool answered;
    private bool quizOngoing = true;

    
    private int currentRound = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("ResultCanvas").GetComponent<Canvas>().enabled = false;
        gameLoop();      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void gameLoop()
    {
        while (quizOngoing)
        {
            for(int i=0; i < options.Count; i++)
            {
                Button currentBtn = options[i];
                currentBtn.onClick.AddListener(() => whenClicked(currentBtn));
            }

            currentRound++;

            if (currentRound==maxQuestionsPerGame)
            {
                quizOngoing = false;
            }
        }
    }

    //activates objects based on QuestionType
    public void SetQuestion(Question question)
    {
        this.question = question;

        switch (this.question.questionType)
        {
            case QuestionType.TEXT:
                questionImage.transform.gameObject.SetActive(false);
                break;
            case QuestionType.IMAGE:
                ImageHolder();
                questionImage.transform.gameObject.SetActive(true);
                questionImage.sprite = question.QuestionImg;
                break;
        }

        questionText.text = question.questionInfo;

        List<string> answerList = RandomizeList.mixTheList<string>(question.options);

        for(int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<Text>().text = answerList[i];
            options[i].name = answerList[i];
            options[i].image.color = neutralColor;
        }

        answered = false;
    }

    void ImageHolder()
    {
        questionImage.transform.parent.gameObject.SetActive(true);
        questionImage.transform.gameObject.SetActive(false);
    }

    //determines answer on click and changes button color
    private void whenClicked(Button button)
    {
        
        if (!answered)
        {
            answered = true;
            bool correctAnswer = quizManager.Answer(button.name, maxQuestionsPerGame);

            if (correctAnswer)
            {
                button.image.color = correctColor;
            }
            else
            {
                button.image.color = wrongColor;
            }

            
        }
    }
}
