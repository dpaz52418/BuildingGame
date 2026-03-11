using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DinosaurHopCactusBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 1f; // speed at which the enemy moves left
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move downwards at a constant speed
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if collides with player, destroy player and indicate a loss.
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Debug.Log("You lose!");
        }
    }
}
