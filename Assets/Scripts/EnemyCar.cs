using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    public float speed = 8f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public float shootingInterval = 2f;
    public float destroyDistance = 30f;

    private float shootingTimer;
    private Transform player;

    void Start()
    {
        // Find the player automatically using the Player tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        // Drive toward the player
        transform.Translate(
            Vector3.back * speed * Time.deltaTime,
            Space.World
        );

        // Shoot periodically
        shootingTimer += Time.deltaTime;

        if (shootingTimer >= shootingInterval)
        {
            Shoot();
            shootingTimer = 0f;
        }

        // Remove the enemy after it passes the player
        if (player != null &&
            transform.position.z < player.position.z - destroyDistance)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null)
        {
            Debug.LogWarning("Enemy bullet prefab is missing.");
            return;
        }

        if (firePoint == null)
        {
            Debug.LogWarning("Enemy fire point is missing.");
            return;
        }

        Quaternion bulletRotation;

        if (player != null)
        {
            // Aim directly at the player
            Vector3 direction = player.position - firePoint.position;
            bulletRotation = Quaternion.LookRotation(direction);
        }
        else
        {
            // Fallback direction if the player cannot be found
            bulletRotation = firePoint.rotation;
        }

        Instantiate(
            bulletPrefab,
            firePoint.position,
            bulletRotation
        );
    }
}
