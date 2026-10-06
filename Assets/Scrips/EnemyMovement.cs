using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;

    [Header("Hit Settings")]
    public int maxHits = 3;
    public float pauseAfterHit = 2f;
    public float knockbackDistance = 3f;

    private int hitCount = 0;
    private float pauseTimer = 0f;
    private bool isPaused = false;

    void Update()
    {
        // If the enemy is paused, count down the timer.
        if (isPaused)
        {
            pauseTimer -= Time.deltaTime;

            if (pauseTimer <= 0f)
            {
                isPaused = false;
            }

            return;
        }

        if (player == null)
            return;

        // Move toward the player.
        Vector3 direction = player.position - transform.position;

        // Don't move up or down.
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            direction.Normalize();

            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isPaused)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            hitCount++;

            Debug.Log("Player hit: " + hitCount + "/" + maxHits);

            // Update the UI.
            HitCounterUI ui = FindFirstObjectByType<HitCounterUI>();

            if (ui != null)
            {
                ui.UpdateHits(hitCount, maxHits);
            }

            // Kill the player after 3 hits.
            if (hitCount >= maxHits)
            {
                SceneManager.LoadScene("Killed");
                return;
            }

            // Push the enemy away from the player.
            Vector3 knockbackDirection = transform.position - player.position;
            knockbackDirection.y = 0f;

            if (knockbackDirection != Vector3.zero)
            {
                knockbackDirection.Normalize();

                transform.position += knockbackDirection * knockbackDistance;
            }

            // PAUSE THE ENEMY.
            isPaused = true;
            pauseTimer = pauseAfterHit;
        }
    }
}
