using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 1f;
    
    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private Transform rightPosition;
    
    private PlayerInput playerInput;
    private int currentPositionIndex = 1; // 0-2 for positions

    void Start()
    {
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
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab not assigned!");
            return;
        }

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
