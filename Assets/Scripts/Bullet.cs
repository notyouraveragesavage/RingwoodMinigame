using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 45f;
    public float lifeTime = 3f;
    public int damage = 1;

    private float timer;

    void Update()
    {
        // Move the bullet forward
        transform.Translate(
            Vector3.forward * speed * Time.deltaTime
        );

        // Destroy the bullet after a few seconds
        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
