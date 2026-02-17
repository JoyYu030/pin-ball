using UnityEngine;

public class breathe : MonoBehaviour
{
    
    [SerializeField] private float breathsPerMinute = 12f;// breaths per minute

    
    [SerializeField] private Vector2 scaleAmount = new Vector2(0.02f, 0.03f);
    [SerializeField] private Vector2 positionAmount = new Vector2(0f, 0.02f);

    
     private float phaseOffset = 0f;
     private float ease = 1.6f;

    private Vector3 startLocalPos;
    private Vector3 startLocalScale;

    void Start()
    {
        startLocalPos = transform.localPosition;
        startLocalScale = transform.localScale;
    }

    void Update()
    {
        float freq = breathsPerMinute / 60f; // cycles per second

        // 0..1 breathing value (0 = exhale, 1 = inhale)
        float raw = (Mathf.Sin((Time.time * freq + phaseOffset) * Mathf.PI * 2f) + 1f) * 0.5f;

        // ease it so it slows at the top/bottom
        float b = Mathf.SmoothStep(0f, 1f, Mathf.Pow(raw, ease));

        //inhale expands/position
        Vector3 targetScale = new Vector3(
            startLocalScale.x * (1f + scaleAmount.x * b),
            startLocalScale.y * (1f + scaleAmount.y * b),
            startLocalScale.z
        );
        Vector3 targetPos = startLocalPos + new Vector3(
            positionAmount.x * b,
            positionAmount.y * b,
            0f
        );

        transform.localScale = targetScale;
        transform.localPosition = targetPos;
    }
}
