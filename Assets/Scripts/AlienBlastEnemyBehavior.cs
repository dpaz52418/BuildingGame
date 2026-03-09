using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlienBlastEnemyBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 1f; // speed at which the enemy moves downwards
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move downwards at a constant speed
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if collides with bullet, destroy self
        if (collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
