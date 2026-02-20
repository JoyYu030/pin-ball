using System.Collections;
using UnityEngine;

public class StomachVomit : MonoBehaviour
{
    [SerializeField] private int hitsToTrigger = 10;
    private int hitCount = 0;
    private bool isPlaying = false;
    private Color hitColor = Color.green;
    private Color finalHitColor = Color.red;
    private float flashDuration = 0.15f;
    private float finalFlashDuration = 0.3f;

    private float vomitTimes = 0;
    [SerializeField] private Animator animator;
    [SerializeField] private string vomitTriggerName = "Vomit";
    [SerializeField] private GameObject vomitAnimator;

    [SerializeField] private AudioClip vomit;
    [SerializeField] private AudioClip burp;

    [Header("Ball Spawning")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnPointA;
    [SerializeField] private Transform spawnPointB;

    private SpriteRenderer sprite;

    private SpriteRenderer sr;
    private Color originalColor;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            originalColor = sr.color;

        sprite = vomitAnimator.GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Ball")) return;
        RegisterHit();
    }

    private void RegisterHit()
    {
        if (isPlaying) return;
        if (vomitTimes >= 3)
        {
            Explode();
            return;
        }

        hitCount++;
        

        if (hitCount >= hitsToTrigger)
        {
            sprite.enabled = true;
            GetComponent<AudioSource>().clip = vomit;
            hitCount = 0;
            StartCoroutine(FinalHitEffect());

            SpawnTwoBalls();
            PlayVomit();
        }
        else
        {
            GetComponent<AudioSource>().clip = burp;
            StartCoroutine(FlashColor(hitColor, flashDuration));
        }
        GetComponent<AudioSource>().Play();
        
    }

    private void PlayVomit()
    {
        
        isPlaying = true;

        if (animator != null)
            animator.SetTrigger(vomitTriggerName);

        Invoke(nameof(EndVomit), 1.0f); // match animation length
        
    }

    private void EndVomit()
    {
        vomitTimes ++;
        isPlaying = false;
        sprite.enabled = false;
    }

    private void Explode()
    {
        gameObject.SetActive(false);
    }
    
    private IEnumerator FlashColor(Color c, float duration)
    {
        sr.color = c;
        yield return new WaitForSeconds(duration);
        sr.color = originalColor;
    }

    private IEnumerator FinalHitEffect()
    {
        sr.color = finalHitColor;
        yield return new WaitForSeconds(finalFlashDuration);
        sr.color = originalColor;
    }

    private void SpawnTwoBalls()
    {
        if (ballPrefab == null || spawnPointA == null || spawnPointB == null)
            return;

        Instantiate(ballPrefab, spawnPointA.position, Quaternion.identity);
        Instantiate(ballPrefab, spawnPointB.position, Quaternion.identity);
    }
}
