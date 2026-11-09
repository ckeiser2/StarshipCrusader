using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [Min(0f)]
    [SerializeField] private float projectileSpeed = 15f;

    [Min(0f)]
    [SerializeField] private float projectileLifetime = 5f;

    [Min(0f)]
    [SerializeField] private int damage = 1;

    private Vector3 direction;

    public float Speed => projectileSpeed;

    public void Initialize(Vector3 shootingDirection)
    {
        direction = shootingDirection.normalized;

        Debug.Log("Projectile Direction: " + direction);

        Destroy(gameObject, projectileLifetime);
    }

    private void Update()
    {
        transform.position +=
            direction * projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}