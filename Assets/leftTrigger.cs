using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftTrigger : MonoBehaviour
{
    private Rigidbody2D rb;
    private float rotateSpeed = 200f;
    private float returnSpeed = 200f;
    private bool canRotate = true;
    private bool reachedTop = false; 

     private HingeJoint2D hinge;
    private bool startFalling = false;
    // Start is called before the first frame update

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hinge = GetComponent<HingeJoint2D>();
    }
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //Debug.Log(reachedTop);
        float maxLimit = hinge.limits.max;

        float currentAngle = hinge.jointAngle;

        float targetAngle = 0f;

        JointMotor2D motor = hinge.motor;

       
        if (Mathf.Abs(currentAngle - targetAngle) > 1f)// decide which direction to rotate
        {
            if (currentAngle > targetAngle)
            {
                
                motor.motorSpeed = -returnSpeed;
            }
            else
            {
                motor.motorSpeed = returnSpeed;
            }
            hinge.motor = motor;
        }
        else
        {
            motor.motorSpeed = 0f;
            hinge.motor = motor;
            if (currentAngle <= 1f && Mathf.Abs(rb.angularVelocity) < 5f )//&& reachedTop)
            {
                canRotate = true;
               // reachedTop = false;
            }   
        
        }
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if(canRotate)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                 rb.AddTorque(rotateSpeed, ForceMode2D.Impulse);
                // rb.AddForce(rotateForce * Vector2.right, ForceMode2D.Impulse);
                canRotate = false;
            }
        }
        
        
    }
}
