using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    // Movement tuning (editable in Inspector)
    public float speed = 5.0f;
    public float turnSpeed;
    // Input System action exposed in Inspector for binding (WASD/Arrow keys)
    public InputAction MoveAction;
    // player inbounds
    public float xRange = 10.0f;
    //Current input value (x = left/right, y = forward/back), kept private for internal use
    private Vector2 moveInput;

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

        // Keep the player inbounds
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if(transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

    }
}
