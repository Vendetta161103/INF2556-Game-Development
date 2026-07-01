using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float patrolDistance = 3f;

    private Vector3 startPos;
    private int direction = 1;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position += Vector3.right * direction * speed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x - startPos.x) >= patrolDistance)
        {
            direction *= -1;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * direction; // Sprite flip in Bewegungsrichtung
            transform.localScale = scale;
        }
    }
}