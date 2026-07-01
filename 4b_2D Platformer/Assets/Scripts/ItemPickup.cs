using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] int scoreValue = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}