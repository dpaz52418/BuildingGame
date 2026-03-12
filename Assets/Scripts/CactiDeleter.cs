using UnityEngine;

public class CactiDeleter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cactus"))
        {
            Destroy(other.gameObject);
        }
    }
}
