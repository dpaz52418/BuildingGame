using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 1f;
    
    private PlayerInput playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
        // If fire point not assigned, use this sprite's position
        if (firePoint == null)
        {
            firePoint = transform;
        }
        
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component not found on this GameObject!");
        }
    }

    public void OnBlastAliens(InputValue value)
    {
        // Shoot when action is performed
        if (value.isPressed)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab not assigned!");
            return;
        }

        // Instantiate bullet at fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        
        // Add velocity to bullet
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = Vector2.up * bulletSpeed;
        }
    }
}
