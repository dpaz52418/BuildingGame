using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScriptLinearSpeed : MonoBehaviour
{
    [SerializeField] private float linearSpeed = 5f;
    [SerializeField] private Rigidbody2D platformRigidbody;
    
    private Rigidbody2D ballRigidbody;
    
    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        
        // Auto-find platform if not assigned
        if (platformRigidbody == null)
        {
            // Try to find platform as parent or via tag
            platformRigidbody = GetComponentInParent<Rigidbody2D>();
            if (platformRigidbody == null)
            {
                platformRigidbody = GameObject.FindGameObjectWithTag("Platform")?.GetComponent<Rigidbody2D>();
            }
        }
        
        if (ballRigidbody == null)
        {
            Debug.LogError("Rigidbody2D not found on ball!");
        }
        if (platformRigidbody == null)
        {
            Debug.LogError("Platform Rigidbody2D not found! Assign it in the inspector or tag it as 'Platform'.");
        }
    }

    void Update()
    {
        if (ballRigidbody == null || platformRigidbody == null) return;
        
        float platformRotation = (platformRigidbody.rotation % 360f + 360f) % 360f;
        
        float moveDirection = 0f;
        if (platformRotation > 0 && platformRotation < 180f)
        {
            
            moveDirection = -1f;
        }
        else if (platformRotation > 180f && platformRotation < 360f)
        {
            
            moveDirection = 1f;
        }
        
        // Apply constant linear velocity along surface, preserve vertical velocity
        ballRigidbody.velocity = new Vector2(moveDirection * linearSpeed, ballRigidbody.velocity.y);
    }
}
