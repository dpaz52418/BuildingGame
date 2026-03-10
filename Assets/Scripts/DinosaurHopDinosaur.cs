using UnityEngine;
using UnityEngine.InputSystem;

public class DinosaurHopDinosaur : MonoBehaviour
{
    [SerializeField] float jumpHeight = 10f;

    Rigidbody2D rb;
    public bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Debug.Log("Rigidbody component found");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void OnJump(InputValue value)
    {
        Debug.Log("Jump input received");
        if (value.isPressed)
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            }
        }
    }
}
