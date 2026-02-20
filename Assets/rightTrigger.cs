using UnityEngine;

public class rightTrigger : MonoBehaviour
{
    [SerializeField] private KeyCode key = KeyCode.D;

    [SerializeField] private float flipSpeed = 1200f;      // motor speed when flipping
    [SerializeField] private float returnSpeed = 800f;     // motor speed when returning
    [SerializeField] private float maxMotorTorque = 20000f;

    [SerializeField] private float angleEpsilon = 1.0f;    // stop when close to target

    private HingeJoint2D hinge;
    private Rigidbody2D rb;

    private float restAngle;   
    private float upAngle;  

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hinge = GetComponent<HingeJoint2D>();

        JointAngleLimits2D lim = hinge.limits;
        restAngle = lim.min;  
        upAngle = lim.max;   

        hinge.useMotor = true;

        
        JointMotor2D m = hinge.motor;
        m.maxMotorTorque = maxMotorTorque;
        hinge.motor = m;

        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.angularDrag = 4f;
    }

    void FixedUpdate()
    {
        bool pressed = Input.GetKey(key);

        float targetAngle;
        float speedMag;

        if (pressed)
        {
            targetAngle = upAngle;
            speedMag = flipSpeed;
        }
        else
        {
            targetAngle = restAngle;
            speedMag = returnSpeed;
        }

        float currentAngle = hinge.jointAngle;
        float diff = targetAngle - currentAngle;

        JointMotor2D motor = hinge.motor;

        // If close enough, stop motor (prevents buzzing)
        if (Mathf.Abs(diff) <= angleEpsilon)
        {
            motor.motorSpeed = 0f;
            hinge.motor = motor;
            return;
        }

        if (diff > 0f)
            motor.motorSpeed = speedMag;
        else
            motor.motorSpeed = -speedMag;

        hinge.motor = motor;
    }
}
