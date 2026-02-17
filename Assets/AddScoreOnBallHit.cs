using UnityEngine;

public class AddScoreOnBallHit : MonoBehaviour
{
    [SerializeField] private int scoreAmount = 100;

    [SerializeField] private float cooldown = 0.05f;
    private float lastHitTime = -999f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Ball")) return;
        TryAddScore();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball")) return;
        TryAddScore();
    }

    private void TryAddScore()
    {
        if (Time.time - lastHitTime < cooldown) return;
        lastHitTime = Time.time;

        if (gameManager.Instance != null)
        {
            gameManager.Instance.AddToScore(scoreAmount);
        }
    }
}
