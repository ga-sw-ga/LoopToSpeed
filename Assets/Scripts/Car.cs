using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
    public static Car instance;
    public float initialSpeed;
    public float driftSpeed;         // How quickly the car moves left/right
    public float dragRate;         // How quickly the car slows down
    public Booster booster;

    private Rigidbody rb;
    private float currentSpeed;
    private Vector3 driftDirection = Vector3.zero;
    private Vector3 lastMousePosition;
    
    // Add this at the top of your script
    private float currentYaw = 0f;                  // current smoothed yaw angle
    private float targetYaw = 0f;                   // target yaw we want to reach
    private float yawVelocity = 0f;                 // velocity reference for SmoothDamp
    private float turnSmoothTime = 0.5f;             // smoothing duration (tweak this!)
    private float maxSpeed = 25f;

    private Transform carModel;
    private TrailRenderer trailRenderer;
    private ParticleSystem fire;
    private float boostTimer = 0f;
    private float boostSpawnTimer = 0f;
    private float boostSpawnDelay = 0.125f;
    private bool isCarAlive = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        carModel = transform.GetChild(0);
        trailRenderer = transform.GetChild(1).GetComponent<TrailRenderer>();
        fire = carModel.GetChild(0).GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        currentSpeed = initialSpeed;
    }

    void Update()
    {
        HandleInput();
        if (isCarAlive)
        {
            ApplyDrag();
        }
        
        if (boostTimer > 0f)
        {
            boostTimer -= Time.deltaTime;
        }
        if (boostSpawnTimer > 0f)
        {
            boostSpawnTimer -= Time.deltaTime;
        }
        else
        {
            Instantiate(booster, transform.position, transform.rotation);
            boostSpawnTimer = boostSpawnDelay;
        }

        if (currentSpeed <= 0.1f)
        {
            UIManager.instance.Fail();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }

    void FixedUpdate()
    {
        if (isCarAlive)
        {
            MoveCar();
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
            if (!UIManager.instance.isFailed)
            {
                isCarAlive = true;
                UIManager.instance.HideStartTip();
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePos = Input.mousePosition;
            float deltaX = Mathf.Clamp((currentMousePos.x - lastMousePosition.x) / 40f, -1f, 1f);

            if (Mathf.Abs(deltaX) > 0.1f)
            {
                driftDirection = new Vector3(deltaX, 0f, 0f);
                //trailRenderer.emitting = true;
            }
            else
            {
                driftDirection = Vector3.zero;
                //trailRenderer.emitting = false;
            }
        }
        else
        {
            driftDirection = Vector3.zero;
            //trailRenderer.emitting = false;
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
        carModel.localEulerAngles = new Vector3(0f, currentYaw * 60f / driftSpeed, 0f);
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
        if (boostTimer <= 0f)
        {
            currentSpeed += amount;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
            fire.Play();
            boostTimer = 1f;
        }
    }

    public void Die()
    {
        isCarAlive = false;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        trailRenderer.emitting = false;
    }
}
