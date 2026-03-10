using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(Image))]
public class ImagePixelSizeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Image image = (Image)target;
        if (GUILayout.Button("Log Actual Pixel Size"))
        {
            LogActualPixelSize(image);
        }
    }

    void LogActualPixelSize(Image image)
    {
        RectTransform rectTransform = image.GetComponent<RectTransform>();
        Canvas canvas = image.GetComponentInParent<Canvas>();

        if (canvas != null)
        {
            // Get the pixel-perfect rectangle
            Rect pixelRect = RectTransformUtility.PixelAdjustRect(rectTransform, canvas);
            Debug.Log($"Actual Pixel Size of {image.name}: Width = {pixelRect.width}, Height = {pixelRect.height}");
        }
        else
        {
            Debug.LogWarning("No Canvas found in parent hierarchy.");
        }
    }
}