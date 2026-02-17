using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plungerVisual : MonoBehaviour
{
    // Start is called before the first frame update
    private KeyCode key = KeyCode.Space;

    private float maxChargeTime = 1.2f;
    private float minSpeed = 3f;
    private float maxSpeed = 20f;

    private Vector2 launchDirection = Vector2.down;

    private float compressedHeightFactor = 0.6f;
    private float compressedYScale = 0.6f;
    private float charge01 = 0f;
    private bool charging = false;

    private bool holdingBall = false;
    private bool launched = false;

    private BoxCollider2D box;
    private Vector2 boxStartSize;

    private Rigidbody2D ballRb;
    private Collider2D ballCol;
     private Vector3 startScale;
    private float ballOriginalGravity;
    private float ballRadius;

    private float ignoreTime = 0.12f;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        boxStartSize = box.size;
        startScale = transform.localScale;
    }

    void Update()
    {
        // if (ballRb == null || launched) return;

        // if (Input.GetKeyDown(key))
        //     charging = true;

        // if (charging && Input.GetKey(key))
        // {
        //     charge01 += Time.deltaTime / maxChargeTime;
        //     charge01 = Mathf.Clamp01(charge01);
        //     UpdateColliderCompression();
        // }

        // if (charging && Input.GetKeyUp(key))
        // {
        //     charging = false;
        //     holdingBall = false;
        //     launched = true;

        //     ballRb.gravityScale = ballOriginalGravity;

        //     float speed = Mathf.Lerp(minSpeed, maxSpeed, charge01);
        //     ballRb.velocity = launchDirection.normalized * speed;
        //     ballRb.angularVelocity = 0f;

        //     charge01 = 0f;
        //     UpdateColliderCompression();

        //     // if (ballCol != null)
        //     // {
        //     //     Physics2D.IgnoreCollision(box, ballCol, true);
        //     //     Invoke(nameof(ReenableCollision), ignoreTime);
        //     // }
        // }
    }

    void FixedUpdate()
    {
        // if (!holdingBall || launched || ballRb == null) return;

        // // ball BELOW plunger: keep it touching bottom face
        // Vector2 targetPos = new Vector2(
        //     ballRb.position.x,
        //     box.bounds.min.y - ballRadius
        // );

        // ballRb.MovePosition(targetPos);
        // ballRb.velocity = Vector2.zero;
        // ballRb.angularVelocity = 0f;
    }

    public void UpdateColliderCompression()
    {
        float yScale = Mathf.Lerp(startScale.y, startScale.y * compressedYScale, charge01);
        transform.localScale = new Vector3(startScale.x, yScale, startScale.z);
    }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (!collision.collider.CompareTag("Ball")) return;
    //     if (launched) return;

    //     ballRb = collision.collider.attachedRigidbody;
    //     if (ballRb == null) return;

    //     ballCol = collision.collider;

    //     ballOriginalGravity = ballRb.gravityScale;
    //     ballRb.gravityScale = 0f;

    //     ballRadius = collision.collider.bounds.extents.y;

    //     holdingBall = true;
    // }

    // void OnCollisionExit2D(Collision2D collision)
    // {
        
    //      if (!collision.collider.CompareTag("Ball")) return;
    //     if (!launched && (holdingBall || charging))// ignore exits caused by collider resizing / tiny separation.
    //         return;

    //     if (ballRb != null)
    //         ballRb.gravityScale = ballOriginalGravity;

    //     ballRb = null;
    //     ballCol = null;

    //     holdingBall = false;
    //     charging = false;
    //     charge01 = 0f;
    //     UpdateColliderCompression();
    // }

    // void ReenableCollision()
    // {
    //     if (ballCol != null)
    //         Physics2D.IgnoreCollision(box, ballCol, false);

    //     launched = false;
    // }
}
