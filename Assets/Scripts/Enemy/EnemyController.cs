using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int contactDamage = 10;
    [SerializeField] private float damageCooldown = 1f;

    private Transform player;
    private Rigidbody rb;
    private float nextDamageTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }

        Vector3 direction = player.position - rb.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
        {
            return;
        }

        direction.Normalize();

        rb.MovePosition(
            rb.position + direction * moveSpeed * Time.fixedDeltaTime
        );

        transform.forward = direction;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Time.time < nextDamageTime)
        {
            return;
        }

        PlayerHealth playerHealth =
            collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(contactDamage);

            nextDamageTime = Time.time + damageCooldown;
        }
    }
}