using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
    public float initialSpeed;
    public float driftSpeed;         // How quickly the car moves left/right
    public float dragRate;         // How quickly the car slows down

    private Rigidbody rb;
    private float currentSpeed;
    private Vector3 driftDirection = Vector3.zero;
    private Vector3 lastMousePosition;
    
    // Add this at the top of your script
    private float currentYaw = 0f;                  // current smoothed yaw angle
    private float targetYaw = 0f;                   // target yaw we want to reach
    private float yawVelocity = 0f;                 // velocity reference for SmoothDamp
    private float turnSmoothTime = 0.35f;             // smoothing duration (tweak this!)

    private List<GameObject> boostBoxes;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = initialSpeed;
    }

    void Update()
    {
        HandleInput();
        ApplyDrag();
    }

    void FixedUpdate()
    {
        MoveCar();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePos = Input.mousePosition;
            float deltaX = Mathf.Clamp((currentMousePos.x - lastMousePosition.x) / 50f, -1f, 1f);

            if (Mathf.Abs(deltaX) > 0.1f)
            {
                driftDirection = new Vector3(deltaX, 0f, 0f);
                
            }
            else
            {
                driftDirection = Vector3.zero;
            }
        }
        else
        {
            driftDirection = Vector3.zero;
        }
    }

    /*void MoveCar()
    {
        Vector3 movement = transform.forward.normalized * currentSpeed + transform.right * (driftDirection.x * driftSpeed);
        rb.velocity = movement;
        transform.Rotate(0f, driftDirection.x * driftSpeed, 0f);
    }*/
    
    void MoveCar()
    {
        // Update targetYaw based on drift direction
        targetYaw = driftDirection.x * driftSpeed; // Accumulate turning angle

        // Smooth the yaw rotation
        currentYaw = Mathf.SmoothDampAngle(currentYaw, targetYaw, ref yawVelocity, turnSmoothTime);
        
        // Movement: Forward + Drift (right vector)
        Vector3 movement = transform.forward.normalized * currentSpeed + transform.right * currentYaw;
        rb.velocity = movement;

        // Apply rotation (around Y-axis)
        transform.Rotate(0f, currentYaw, 0f);
        //transform.rotation = Quaternion.Euler(0f, currentYaw, 0f);
    }


    void ApplyDrag()
    {
        currentSpeed -= dragRate * Time.deltaTime;
        currentSpeed = Mathf.Max(0f, currentSpeed);
    }

    // Call this externally to add a speed boost after a loop
    public void Boost(float amount)
    {
        currentSpeed += amount;
    }
}
