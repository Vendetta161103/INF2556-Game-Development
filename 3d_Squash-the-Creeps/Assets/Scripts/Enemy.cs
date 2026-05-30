using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] Animator animator;
    private float lifetime = 20f; 
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;
        transform.position += dir * speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(dir);

        lifetime -= Time.deltaTime;
        if (lifetime <= 0f){
            Destroy(gameObject);
        }
    }

    // Spieler springt drauf → Stomp
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc == null) return;

        // Foot Transform vom PlayerController holen
        Transform foot = pc.GetFoot();

        float footY = foot.position.y;
        float enemyY = transform.position.y;
        float difference = footY - enemyY;

        if (difference > 0.1f && pc.GetJumpsLeft() == 0){
            GameManager.Instance.AddScore(1);
            Destroy(gameObject);
        }
        else{
            pc.Die();
        }
    }
}