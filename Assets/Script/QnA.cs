using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QnAChoiceWithSubmit : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string englishWord;        // English word (e.g., Cat)
        public string[] tamilOptions;     // 4 Tamil options
        public int correctIndex;          // Correct answer index
        public AudioClip[] optionAudio;   // 4 audio clips (Tamil pronunciations)
    }

    public Question[] questions;
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;        // Assign 4 buttons
    public TextMeshProUGUI feedbackText;
    public Button submitButton;           // Submit button
    public AudioSource audioSource;

    private int currentIndex = 0;
    private int selectedIndex = -1;       // which option is chosen

    void Start()
    {
        submitButton.onClick.AddListener(OnSubmit);
        LoadQuestion();
    }

    void LoadQuestion()
    {
        if (currentIndex < questions.Length)
        {
            Question q = questions[currentIndex];
            questionText.text = "Translate: " + q.englishWord + " ?";
            feedbackText.text = "";
            selectedIndex = -1; // reset choice

            for (int i = 0; i < optionButtons.Length; i++)
            {
                int index = i;
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.tamilOptions[i];

                // clear old listeners
                optionButtons[i].onClick.RemoveAllListeners();

                // add new listener: play audio & mark selection
                optionButtons[i].onClick.AddListener(() => OnOptionSelect(index));
            }
        }
        else
        {
            questionText.text = "?? Game Over! Well done!";
            feedbackText.text = "";
            foreach (Button btn in optionButtons) btn.gameObject.SetActive(false);
            submitButton.gameObject.SetActive(false);
        }
    }

    void OnOptionSelect(int index)
    {
        selectedIndex = index;

        // play pronunciation
        Question q = questions[currentIndex];
        if (q.optionAudio[index] != null)
        {
            audioSource.PlayOneShot(q.optionAudio[index]);
        }

        // highlight selection (optional: change button color)
        foreach (Button btn in optionButtons)
            btn.image.color = Color.white; // reset

        optionButtons[index].image.color = Color.yellow; // selected
    }

    void OnSubmit()
    {
        if (selectedIndex == -1)
        {
            feedbackText.text = "?? Please choose an option!";
            return;
        }

        Question q = questions[currentIndex];

        if (selectedIndex == q.correctIndex)
        {
            feedbackText.text = "? Correct!";
            currentIndex++;
            Invoke("LoadQuestion", 2f);
        }
        else
        {
            feedbackText.text = "? Wrong, try again!";
        }
    }
}
