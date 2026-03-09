using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShipShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 1f;
    
    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private Transform rightPosition;

    [Header("Cooldown Meter")]
    [SerializeField] private float maxMeter = 100f;
    [SerializeField] private float refillRate = 25f; // meter units per second
    [SerializeField] private float shotCost = 35f; // meter units consumed per shot
    [SerializeField] private Image meterFillImage; // UI Image (filled) to display the meter

    private float currentMeter;
    private PlayerInput playerInput;
    private int currentPositionIndex = 1; // 0-2 for positions

    /// Returns 0-1 meter value. => is a "get" accessor, which computes the expression every time
    /// meterpercent is used or read.
    public float MeterPercent => currentMeter / maxMeter;

    void Start()
    {
        currentMeter = maxMeter;
        playerInput = GetComponent<PlayerInput>();
        
        // firepoint is this position
        if (firePoint == null)
        {
            firePoint = transform;
        }
        
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component not found");
        }
        
        // start at center
        if (centerPosition != null)
        {
            transform.position = centerPosition.position;
        }
    }

    void Update()
    {
        // constantly refill the meter (clamped to max)
        if (currentMeter < maxMeter)
        {
            currentMeter = Mathf.Min(currentMeter + refillRate * Time.deltaTime, maxMeter);
        }

        // keep the UI bar in sync
        if (meterFillImage != null)
        {
            meterFillImage.fillAmount = MeterPercent;
        }
    }

    public void OnBlastAliens(InputValue value)
    {
        if (value.isPressed)
        {
            Shoot();
        }
    }

    public void OnMoveLeft(InputValue value)
    {
        if (value.isPressed)
        {
            MoveLeft();
        }
    }

    public void OnMoveRight(InputValue value)
    {
        if (value.isPressed)
        {
            MoveRight();
        }
    }

    void MoveLeft()
    {
        if (currentPositionIndex > 0)
        {
            currentPositionIndex--;
            UpdatePosition();
        }
    }

    void MoveRight()
    {
        if (currentPositionIndex < 2)
        {
            currentPositionIndex++;
            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        Transform targetPosition = null;
        
        switch (currentPositionIndex)
        {
            case 0:
                targetPosition = leftPosition;
                break;
            case 1:
                targetPosition = centerPosition;
                break;
            case 2:
                targetPosition = rightPosition;
                break;
        }
        
        if (targetPosition != null)
        {
            transform.position = targetPosition.position;
        }
        else
        {
            Debug.LogError("where are you going lol");
        }
    }

    void Shoot()
    {
        if (currentMeter < shotCost)
        {
            return;
        }

        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab not assigned!");
            return;
        }

        currentMeter -= shotCost;

        // instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        
        // bullet velocity
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = Vector2.up * bulletSpeed;
        }
    }

    
}
