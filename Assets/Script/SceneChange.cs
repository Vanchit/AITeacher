using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Diagnostics;

public class PanelButton : MonoBehaviour, IPointerClickHandler
{
    [Header("Enter the scene name to load")]
    public string sceneToLoad;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            UnityEngine.Debug.LogWarning("No scene name set for panel: " + gameObject.name);
        }
    }
}
