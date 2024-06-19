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


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        characterActions = new MyInputActions();
        characterActions.Enable();

        InputSystem.onDeviceChange += OnDeviceChange;

    }

    // Update is called once per frame
    void Update()
    {
        if(!gamepadIsConnected)
        {
            UpdateAimWithMouse();
        }
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        // Update input devices when a device is added or removed
        if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed)
        {
            foreach(var devices in InputSystem.devices){
            if(devices is Gamepad){
            gamepadIsConnected = true;
            break;
            }
        }
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
        rb.AddForce(aim * jumpForce);
    }
}
