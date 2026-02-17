using System.Collections;
using UnityEngine;
using UnityEngine.UI; // for CanvasGroup

public class StomachVomit : MonoBehaviour
{

    [SerializeField] private int hitsToTrigger = 10;
    private int hitCount = 0;
    private bool isPlaying = false;

    //[Header("Vomit Overlay (UI)")]
    //[SerializeField] private CanvasGroup vomitOverlay; // UI image covering screen with CanvasGroup
    [SerializeField] private float fadeInTime = 0.15f;
    [SerializeField] private float holdTime = 0.6f;
    [SerializeField] private float fadeOutTime = 0.35f;

    [Header("Ball Spawning")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnPointA;
    [SerializeField] private Transform spawnPointB;
    [SerializeField] private float spawnDelayAfterVomitStarts = 0.1f;

    void Start()
    {
        // if (vomitOverlay != null)
        // {
        //     vomitOverlay.alpha = 0f;
        //     vomitOverlay.blocksRaycasts = false;
        //     vomitOverlay.interactable = false;
        // }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Ball")) return;
        RegisterHit();
    }


    private void RegisterHit()
    {
        if (isPlaying) return; // don't count hits during vomit sequence

        hitCount++;

        if (hitCount >= hitsToTrigger)
        {
            hitCount = 0; 
           // StartCoroutine(VomitSequence());
        }
    }

    // private IEnumerator VomitSequence()
    // {
    //     isPlaying = true;

    //     // Fade overlay in
    //     if (vomitOverlay != null)
    //         yield return FadeCanvasGroup(vomitOverlay, 0f, 1f, fadeInTime);

    //     // Spawn balls shortly after vomit starts
    //     yield return new WaitForSeconds(spawnDelayAfterVomitStarts);
    //     SpawnTwoBalls();

    //     // Hold overlay
    //     yield return new WaitForSeconds(holdTime);

    //     // Fade overlay out
    //     if (vomitOverlay != null)
    //         yield return FadeCanvasGroup(vomitOverlay, 1f, 0f, fadeOutTime);

    //     isPlaying = false;
    // }

    private void SpawnTwoBalls()
    {
        // if (ballPrefab == null)
        // {
        //     Debug.LogWarning("StomachVomit: ballPrefab not assigned.");
        //     return;
        // }
        // if (spawnPointA == null || spawnPointB == null)
        // {
        //     Debug.LogWarning("StomachVomit: spawn points not assigned.");
        //     return;
        // }

        Instantiate(ballPrefab, spawnPointA.position, Quaternion.identity);
        Instantiate(ballPrefab, spawnPointB.position, Quaternion.identity);
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float from, float to, float duration)
    {
        if (cg == null) yield break;

        cg.alpha = from;

        if (duration <= 0f)
        {
            cg.alpha = to;
            yield break;
        }

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float k = Mathf.Clamp01(t / duration);
            cg.alpha = Mathf.Lerp(from, to, k);
            yield return null;
        }

        cg.alpha = to;
    }
}
