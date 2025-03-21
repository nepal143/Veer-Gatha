using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QnAManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] options = new string[4];
        public int correctAnswerIndex;
    }

    public Question[] questions;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] optionTexts;
    public Button[] optionButtons;
    public GameObject quizPanel;
    public Button startButton;
    
    private int currentQuestionIndex;

    void Start()
    {
        quizPanel.SetActive(false);
        startButton.onClick.AddListener(StartQuiz);
    }

    public void StartQuiz()
    {
        startButton.gameObject.SetActive(false);
        quizPanel.SetActive(true);
        currentQuestionIndex = 0;
        LoadQuestion();
    }

    void LoadQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            Debug.Log("Quiz Completed");
            return;
        }
        
        Question q = questions[currentQuestionIndex];
        questionText.text = q.questionText;
        
        for (int i = 0; i < optionTexts.Length; i++)
        {
            optionTexts[i].text = q.options[i];
            optionButtons[i].image.color = Color.white; // Reset button color
            int index = i; 
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
    }

    void CheckAnswer(int selectedIndex)
    {
        Question q = questions[currentQuestionIndex];
        
        if (selectedIndex == q.correctAnswerIndex)
        {
            optionButtons[selectedIndex].image.color = Color.green;
        }
        else
        {
            optionButtons[selectedIndex].image.color = Color.red;
            optionButtons[q.correctAnswerIndex].image.color = Color.green;
        }

        StartCoroutine(NextQuestion());
    }

    IEnumerator NextQuestion()
    {
        yield return new WaitForSeconds(2f);
        currentQuestionIndex++;
        LoadQuestion();
    }
}
