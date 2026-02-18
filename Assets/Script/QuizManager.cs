using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;
    public Button submitButton;

    [Header("Quiz Data")]
    public Question[] questions;  // Fill this in Inspector
    private int currentQuestionIndex = 0;

    private int selectedIndex = -1;

    void Start()
    {
        LoadQuestion();
        submitButton.onClick.AddListener(CheckAnswer);

        // Add listeners to option buttons
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i; // local copy
            optionButtons[i].onClick.AddListener(() => SelectAnswer(index));
        }
    }

    void LoadQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            Debug.Log("Quiz Finished!");
            questionText.text = "You finished the quiz ??";
            submitButton.interactable = false;
            return;
        }

        Question q = questions[currentQuestionIndex];

        questionText.text = q.questionText;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.options[i];
            optionButtons[i].interactable = true;
            optionButtons[i].GetComponent<Image>().color = Color.white;
        }

        selectedIndex = -1;
        submitButton.interactable = true;
    }

    void SelectAnswer(int index)
    {
        // reset colors
        foreach (Button btn in optionButtons)
            btn.GetComponent<Image>().color = Color.white;

        // highlight selected
        optionButtons[index].GetComponent<Image>().color = Color.yellow;
        selectedIndex = index;
    }

    void CheckAnswer()
    {
        if (selectedIndex == -1) return; // nothing selected

        Question q = questions[currentQuestionIndex];

        if (selectedIndex == q.correctIndex)
        {
            optionButtons[selectedIndex].GetComponent<Image>().color = Color.green;

            // ? Go to next question after short delay
            Invoke("NextQuestion", 1.2f);
        }
        else
        {
            optionButtons[selectedIndex].GetComponent<Image>().color = Color.red;
            optionButtons[selectedIndex].interactable = false; // lock wrong one
        }
    }

    void NextQuestion()
    {
        currentQuestionIndex++;
        LoadQuestion();
    }
}
