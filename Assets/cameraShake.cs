using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPos;

    private float shakeTime = 0f;
    private float shakeStrength = 0.1f;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (shakeTime > 0f)
        {
            transform.localPosition = originalPos + (Vector3)Random.insideUnitCircle * shakeStrength;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0f;
            transform.localPosition = originalPos;
        }
    }

    
    public void Shake(float duration, float strength)
    {
        shakeTime = duration;
        shakeStrength = strength;
    }
}
