using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UICrank : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f; // degrees per second
    [SerializeField] private Image fillImage; // UI Image (Filled, Radial 360)

    private RectTransform rectTransform;
    private int rotationDirection = -1; // -1 = clockwise, 1 = counter-clockwise (Z-axis)

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.Rotate(0f, 0f, rotationDirection * rotationSpeed * Time.deltaTime);

        if (fillImage != null)
        {
            // map the crank's current Z rotation (0-360) to fill amount (0-1)
            float zAngle = rectTransform.localEulerAngles.z;
            fillImage.fillAmount = zAngle / 360f;
        }
    }

    public void OnRotateLeft(InputValue value)
    {
        if (value.isPressed)
        {
            rotationDirection = 1;
        }
    }

    public void OnRotateRight(InputValue value)
    {
        if (value.isPressed)
        {
            rotationDirection = -1;
        }
    }
}
