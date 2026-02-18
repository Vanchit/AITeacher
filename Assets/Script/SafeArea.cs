using UnityEngine;

/// <summary>
/// Adjusts the panel to fit inside the safe area of screens (handles notches, rounded corners, etc.)
/// Attach this script to a full-screen UI Panel under your Canvas.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour
{
    RectTransform panel;
    Rect lastSafeArea = new Rect(0, 0, 0, 0);

    void Awake()
    {
        panel = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    void Update()
    {
        // Some devices (like Android with gesture nav) can change safe area at runtime
        if (Screen.safeArea != lastSafeArea)
            ApplySafeArea();
    }

    void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;
        lastSafeArea = safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        panel.anchorMin = anchorMin;
        panel.anchorMax = anchorMax;
    }
}
