
using UnityEngine;

public class TurretEnemy : Enemy
{
    [Header("Detection Settings")]
    [Min(0f)]
    [SerializeField] private float detectionRadius = 50f;

    [Header("Shooting Settings")]
    [Min(0.01f)]
    [SerializeField] private float fireRate = 2f;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    [Header("Targeting Settings")]
    [Min(0f)]
    [SerializeField] private float maxPredictionTime = 2f;

    private Transform playerTarget;

    private Vector3 previousPlayerPosition;
    private Vector3 playerVelocity;

    private float fireTimer;

    private void Awake()
    {
        GameObject playerGO = GameObject.FindWithTag("Player");

        if (playerGO == null)
        {
            Debug.LogWarning("Turret: Player not found!");
            return;
        }

        playerTarget = playerGO.transform;
        previousPlayerPosition = playerTarget.position;
    }

    protected override void Start()
    {
        base.InitializeEnemy();

        gameObject.name = enemyData.EnemyName;
    }

    protected override void UpdateBehavior()
    {
        if (playerTarget == null)
            return;

        // Measure the player's world-space movement.
        Vector3 currentPosition = playerTarget.position;

        if (Time.deltaTime > 0f)
        {
            playerVelocity =
                (currentPosition - previousPlayerPosition) / Time.deltaTime;

            // Only use X/Y movement for predictive aiming.
            playerVelocity.z = 0f;
        }

        previousPlayerPosition = currentPosition;

        // Check whether player is within detection range.
        if (!IsPlayerInRange())
        {
            fireTimer = 0f;
            return;
        }

        // Handle firing interval.
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            ShootAtPlayer();
            fireTimer = 0f;
        }
    }

    private bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(
            transform.position,
            playerTarget.position
        );

        return distance <= detectionRadius;
    }

    private Vector3 CalculateShootingDirection(float projectileSpeed)
    {
        Vector3 turretPosition = firePoint.position;
        Vector3 targetPosition = playerTarget.position;

        // Calculate the current distance and direction to the player.
        Vector3 relativePosition = targetPosition - turretPosition;

        // Only predict player movement along X/Y.
        Vector3 targetVelocity = new Vector3(
            playerVelocity.x,
            playerVelocity.y,
            0f
        );

        // Solve the projectile interception equation.
        float a = Vector3.Dot(targetVelocity, targetVelocity)
                  - projectileSpeed * projectileSpeed;

        float b = 2f * Vector3.Dot(relativePosition, targetVelocity);

        float c = Vector3.Dot(relativePosition, relativePosition);

        float discriminant = b * b - 4f * a * c;

        float interceptTime = 0f;

        if (Mathf.Abs(a) < 0.001f)
        {
            if (Mathf.Abs(b) > 0.001f)
                interceptTime = -c / b;
        }
        else if (discriminant >= 0f)
        {
            float sqrtDiscriminant = Mathf.Sqrt(discriminant);

            float t1 = (-b - sqrtDiscriminant) / (2f * a);
            float t2 = (-b + sqrtDiscriminant) / (2f * a);

            if (t1 > 0f && t2 > 0f)
                interceptTime = Mathf.Min(t1, t2);
            else if (t1 > 0f)
                interceptTime = t1;
            else if (t2 > 0f)
                interceptTime = t2;
        }

        // If interception is impossible, aim directly at the player.
        if (interceptTime <= 0f)
            return relativePosition.normalized;

        // Prevent excessive prediction.
        interceptTime = Mathf.Min(interceptTime, maxPredictionTime);

        // Predict the player's future position.
        Vector3 predictedPosition =
            targetPosition + targetVelocity * interceptTime;

        // Return the direction toward the predicted position.
        return (predictedPosition - turretPosition).normalized;
    }

    private void ShootAtPlayer()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Turret: Projectile Prefab is missing!");
            return;
        }

        if (firePoint == null)
        {
            Debug.LogError("Turret: FirePoint is missing!");
            return;
        }

        TurretProjectile projectileScript =
            projectilePrefab.GetComponent<TurretProjectile>();

        if (projectileScript == null)
        {
            Debug.LogError("Turret: Projectile is missing TurretProjectile.cs!");
            return;
        }

        // Calculate the predicted shooting direction.
        Vector3 shootingDirection =
            CalculateShootingDirection(projectileScript.Speed);

        // Spawn the projectile independently of the scrolling platform.
        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.identity
        );

        TurretProjectile spawnedProjectile =
            projectile.GetComponent<TurretProjectile>();

        spawnedProjectile.Initialize(shootingDirection);

        // Debug information.
        Debug.Log(
            $"Turret Position: {firePoint.position}\n" +
            $"Player Position: {playerTarget.position}\n" +
            $"Player Velocity: {playerVelocity}\n" +
            $"Calculated Direction: {shootingDirection}"
        );

        Debug.DrawRay(
            firePoint.position,
            shootingDirection * 20f,
            Color.green,
            2f
        );
    }

    protected override EnemyData CreateEnemyData()
    {
        return EnemyProfiles.CreateTurret();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            detectionRadius
        );
    }
}
