using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballHit : MonoBehaviour
{
    [SerializeField] private AudioClip lose;
    
    // [SerializeField]private CameraShake cameraShake;
    private bool played = false;
    private CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -11 && !played)
        {
            GetComponent<AudioSource>().clip = lose;
            GetComponent<AudioSource>().Play();
            played = true;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Plunger"))
            return;
        GetComponent<AudioSource>().Play();

        if (cameraShake != null)
            cameraShake.Shake(0.04f, 0.08f);
    }
}
