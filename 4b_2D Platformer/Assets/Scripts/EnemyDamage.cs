using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int damageValue = 2;
    [SerializeField] float damageCooldown = 1f;

    private float lastDamageTime = -999f;

    void OnTriggerEnter2D(Collider2D other)
    {
        TryDamage(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        TryDamage(other);
    }

    void TryDamage(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (Time.time - lastDamageTime < damageCooldown) return;

        lastDamageTime = Time.time;
        ScoreManager.Instance.AddScore(-damageValue);
    }
}