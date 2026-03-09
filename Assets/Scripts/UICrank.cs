using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UICrank : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f; // degrees per second

    private RectTransform rectTransform;
    private int rotationDirection = -1; // -1 = clockwise, 1 = counter-clockwise (Z-axis)

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.Rotate(0f, 0f, rotationDirection * rotationSpeed * Time.deltaTime);
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
