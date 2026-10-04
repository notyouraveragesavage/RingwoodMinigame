using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    // Movement tuning (editable in Inspector)
    public float speed = 5.0f;
    public float turnSpeed;
    // Input System action exposed in Inspector for binding (WASD/Arrow keys)
    public InputAction MoveAction;
    //Current input value (x = left/right, y = forward/back), kept private for internal use
    private Vector2 moveInput;

    public GameObject bulletPrefab;
    public Transform firepoint;
    public float fireRate = 0.25f;

    private float nextFireTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Enable the MoveAction so it starts reading input
        MoveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // Read the 2D vector from the MoveAction (x: horizontal, y: vertical)
        moveInput = MoveAction.ReadValue<Vector2>();
        
        // Move the vehicle forward/back along local Z using the y component
        transform.Translate(Vector3.forward * Time.deltaTime * speed * moveInput.y);

        // Rotate around the local Y (yaw) using the x component
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * moveInput.x);

        // Shoot with SPACE or LMB
        if((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Instantiate(
            bulletPrefab,
            firepoint.position,
            firepoint.rotation
        );
    }
}
