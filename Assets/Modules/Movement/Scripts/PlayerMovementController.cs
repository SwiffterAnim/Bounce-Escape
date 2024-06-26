using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{

    [SerializeField] private float jumpForce = 350f;

    private bool gamepadIsConnected = false;

    private Camera mainCamera;
    private Rigidbody2D rb;
    private MyInputActions characterActions;

    private Vector2 aim;
    private bool canJump = true;


    [SerializeField] private GameObject testerColorBOX;
    [SerializeField] private Color on;
    [SerializeField] private Color off;
    private Color boxColor;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        characterActions = new MyInputActions();
        characterActions.Enable();

        //this asks to run this method OnDeviceChange when the device changes. Interesting concept.
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    // Update is called once per frame
    void Update()
    {
        // I'm trying to test if gamepadIsConnected is actually changing, but it doesn't look like it is.
        if(!gamepadIsConnected)
        {
            // UpdateAimWithMouse();
            testerColorBOX.GetComponent<SpriteRenderer>().color = off;
        }
        else{
            testerColorBOX.GetComponent<SpriteRenderer>().color = on;
        }
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        // Update input devices when a device is added or removed
        if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed)
        {
            gamepadIsConnected = device is Joystick; //same as if(device is Gamepad){gamepadIsConnected = true;}
        }
    }


    private void UpdateAimWithMouse()
    {
        Vector2 playerPosition = new Vector2 (transform.position.x, transform.position.y);
        Vector2 mousePositionScreen = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        aim = mousePositionScreen - playerPosition;
        aim.Normalize();
    }

    private void OnAim(InputValue value)
    {
        aim = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (aim == Vector2.zero)
        {
            aim.y = 1f;
        }
        if (canJump)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(aim * jumpForce);
            canJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        canJump = true;
    }

}
