using UnityEngine;

public class MicrogameLoseZone : MonoBehaviour
{
    [SerializeField] private string targetTag = "Ball";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            MicrogameManager.NotifyLoss(gameObject);
            Debug.Log("Object with tag '" + targetTag + "' entered lose zone, loss triggered.");
        }
    }
}
