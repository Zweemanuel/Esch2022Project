using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quizmanager : MonoBehaviour
{
    [SerializeField] private QuizUI quizUI;
    [SerializeField]
    private List<Question> questions;
    private Question currentQuestion;
    private int rightAnswers = 0;
    private int currQuestionIndex = 0;
    private int max;
    

    // Start is called before the first frame update
    void Start()
    {
        ChooseQuestion(); 
    }
    

    //chooses next question
    void ChooseQuestion()
    {   
        //random with repeat on used indexes
        bool lookForUnusedQuestionFlag = true;

        while (lookForUnusedQuestionFlag)
        {
            int questionIndex = Random.Range(0, questions.Count);
            if (questions[questionIndex].alreadyUsed == false)
            {
                questions[questionIndex].alreadyUsed = true;
                currentQuestion = questions[questionIndex];
                quizUI.SetQuestion(currentQuestion);
                lookForUnusedQuestionFlag = false;
                
            }
        }
    }

    //determines and returns answer and calls choosequestion method
    public bool Answer(string answered, int maxQuestions)
    {
        currQuestionIndex++;
        max = maxQuestions;
        bool correctAnswer = false;
        if (answered == currentQuestion.correctAnswer)
        {
            //correct Answer
            correctAnswer = true;
            rightAnswers++;
        }
        else
        {
            //wrong Answer
        }

        if (currQuestionIndex < maxQuestions)
        {
            Invoke("ChooseQuestion", 0.5f);
        }
        else
        {   
            Invoke("ResultScreen", 0.5f);
        }
       

        return correctAnswer;
    }


    //some method
    void ResultScreen()
    {
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        GameObject.Find("ResultCanvas").GetComponent<Canvas>().enabled = true;

        string resultText = "PlaceHolderText";
        GameObject.Find("CongratsText").GetComponentInChildren<Text>().text = "Congratulations!\n You answered "+rightAnswers+" of "+max+" right!";

        switch (rightAnswers)
        {
            case 0:
                resultText = "You didn't do so well this time.\nBetter Luck next time!";
                break;
            case 1:
            case 2:
                resultText = "You answered some questions right.\nYou're nearly there!";
                break;
            case 3:
            case 4:
                resultText = "Wow! You almost answered all of your Questions correctly!\nCan you do answer them all?";
                break;

            case 5:
                resultText = "Congratulations!\n You answered all of your questions right!";
                break;
            default:
                break;
        }

        GameObject.Find("CongratsText").GetComponentInChildren<Text>().text = resultText;
    }

    //Update is called once per frame
    void Update()
    {
        
    }

}

[System.Serializable]
public class Question
{
    public string questionInfo;
    public List<string> options;
    public string correctAnswer;
    public QuestionType questionType;
    public Sprite QuestionImg;
    public bool alreadyUsed = false;
}

[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE
}