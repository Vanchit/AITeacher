using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public bool isCorrect = false;   // Tick this in Inspector for the correct option
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        UnityEngine.UI.Image img = GetComponent<UnityEngine.UI.Image>();

        if (isCorrect)
        {
            // ✅ Correct → turn green & disable button
            img.color = Color.green;
            button.interactable = false;
        }
        else
        {
            // ❌ Wrong → turn red, but still allow other clicks
            img.color = Color.red;
            button.interactable = false; // Only this wrong one is locked
        }
    }
}
