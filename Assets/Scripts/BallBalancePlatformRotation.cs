using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallBalancePlatformRotation : MonoBehaviour
{
    [SerializeField] private float maxTiltTorque = 10f;
    [SerializeField] private float torqueSpeed = 5f;
    [SerializeField] private float releasedTorque = 3f;

    private Rigidbody2D platformRigidbody;
    private Vector2 currentInput = Vector2.zero;

    private void Start()
    {
        platformRigidbody = GetComponent<Rigidbody2D>();
        if (platformRigidbody == null)
        {
            Debug.LogError("Rigidbody2D component not found on platform!");
        }
    }

    public void OnControlPlatform(InputValue value)
    {
        currentInput = value.Get<Vector2>();
    }

    void Update()
    {
        if (platformRigidbody == null) return;

        // calc target torque based on input
        float targetTorque = -currentInput.x * maxTiltTorque;

        if (Mathf.Abs(currentInput.x) > 0.1f)
        {
            // input
            float torqueToApply = Mathf.Lerp(platformRigidbody.angularVelocity, targetTorque, torqueSpeed * Time.deltaTime);
            platformRigidbody.angularVelocity = torqueToApply;
        }
        else
        {
            // no input
            float normalizedRotation = (platformRigidbody.rotation % 360f + 360f) % 360f;
            
            float continuedTorque = 0f;
            if (normalizedRotation > 0 && normalizedRotation < 180f)
            {
                // pos dir
                continuedTorque = releasedTorque;
            }
            else if (normalizedRotation > 180f && normalizedRotation < 360f)
            {
                // pos dir
                continuedTorque = -releasedTorque;
            }
            
            // continue plat
            platformRigidbody.angularVelocity = Mathf.Lerp(platformRigidbody.angularVelocity, continuedTorque, releasedTorque * Time.deltaTime);
        }
    }
}