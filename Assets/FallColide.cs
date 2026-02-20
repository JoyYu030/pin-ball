// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(SpriteRenderer))]//add a sprite renderer component, but u still need to add the sprite
// [RequireComponent(typeof(BoxCollider2D))]
public class FallColide : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] float forceValue = 100.0f;
    // void Awake()// first thing that runs/ great for initialization
    // {
    //     spriteRenderer = GetComponent<SpriteRenderer>();
    // }
    // void ChangeColor()
    // {
    //     spriteRenderer.color = new Color(Random.value, Random.value, Random.value);
    // }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<AudioSource>().Play();
        //ChangeColor();
        collision.rigidbody.AddForce(forceValue * Vector2.up, ForceMode2D.Impulse);// impulse: instant force
        // gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
