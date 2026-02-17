using UnityEngine;

public class heartBeat: MonoBehaviour
{

    [SerializeField] private float baseBpm = 90f;
    [SerializeField] private float baseStrength = 0.10f; // 0.10 = +10% scale


    [SerializeField] private float maxBpm = 180f;
    [SerializeField] private float maxStrength = 0.25f;

    [SerializeField] private float bpmAddPerHit = 8f;
    [SerializeField] private float strengthAddPerHit = 0.02f;


    private float calmDelay = 5f;     
     private float calmSmooth = 3f;      

    private float sharpness = 6f;       

    private float currentBpm;
    private float currentStrength;

    private float lastHitTime = -999f;
    private float maxTimer = 0f;

    private float maxHoldTime = 5f;
    private Vector3 baseScale;

    void Start()
    {
        baseScale = transform.localScale;
        currentBpm = baseBpm;
        currentStrength = baseStrength;
    }

    void Update()
    {
        bool shouldCalm = Time.time - lastHitTime >= calmDelay;

        if (shouldCalm)
        {
            currentBpm = Mathf.Lerp(currentBpm, baseBpm, Time.deltaTime * calmSmooth);
            currentStrength = Mathf.Lerp(currentStrength, baseStrength, Time.deltaTime * calmSmooth);
        }

       
        float freq = currentBpm / 60f;
        float t = Time.time * freq;

        float beat = Mathf.Clamp01(Pulse(t) + 0.55f * Pulse(t + 0.25f));

        float s = 1f + beat * currentStrength;
        transform.localScale = baseScale * s;


        bool atMax =
            Mathf.Approximately(currentBpm, maxBpm) &&
            Mathf.Approximately(currentStrength, maxStrength);

        if (atMax)
        {
            maxTimer += Time.deltaTime;

            if (maxTimer >= maxHoldTime)
            {
                GetComponent<AudioSource>().Play();
                gameObject.SetActive(false);
                
                return;
            }
        }
        else
        {
            maxTimer = 0f; // reset if either drops
        }
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
            OnBallHit();
    }


    private void OnBallHit()
    {
        lastHitTime = Time.time;

        currentBpm = Mathf.Min(maxBpm, currentBpm + bpmAddPerHit);
        currentStrength = Mathf.Min(maxStrength, currentStrength + strengthAddPerHit);
    }

    private float Pulse(float x)
    {
        float sine01 = (Mathf.Sin(x * Mathf.PI * 2f) + 1f) * 0.5f; // 0..1
        return Mathf.Pow(sine01, sharpness);
    }
}
