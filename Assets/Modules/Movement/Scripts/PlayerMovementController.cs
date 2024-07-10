using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{

    [SerializeField] private float jumpForce = 350f;
    [SerializeField] private PlayerCollisionController playerCollisionController;
    [SerializeField] private PlayerLifeController playerLifeController;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private TimeManager timeManager;

    private bool gamepadIsConnected = false;

    private Camera mainCamera;
    private Rigidbody2D rb;
    private MyInputActions characterActions;

    private Vector2 aim;
    private bool canJump = true;


    private void Awake()
    {
        playerCollisionController.OnCollisionEnter2DEvent += OnCollisionEnter2DEvent;
  
    }

    private void OnDestroy()
    {
        playerCollisionController.OnCollisionEnter2DEvent -= OnCollisionEnter2DEvent;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        characterActions = new MyInputActions();
        characterActions.Enable();

        //this asks to run this method OnDeviceChange when the device changes. Interesting concept.
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    void Update()
    {
        // I'm trying to test if gamepadIsConnected is actually changing, but it doesn't look like it is.
        if(!gamepadIsConnected)
        {
            //UpdateAimWithMouse();
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
        timeManager.CancelSlowMotion();
        rb.isKinematic = false;
        rb.WakeUp();
        if (canJump)
        {
            if (aim == Vector2.zero)
            {
                aim.y = 1f;
            }
            rb.velocity = Vector2.zero;
            rb.AddForce(aim * jumpForce);
            canJump = false;
        }
    }

    private void OnCollisionEnter2DEvent(Collision2D other)
    {
        if(playerLifeController.isAlive)
        {
            canJump = true;
            CameraManager.Instance.Shake();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ObstacleEntity obstacle))
        {
            if (obstacle.hooknessActivated && playerLifeController.isAlive)
            {
                canJump = true;
                transform.position = Vector2.Lerp(other.gameObject.transform.position, transform.position, obstacle.attractionSpeed * Time.deltaTime);
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
                rb.Sleep();
            }
        }
    }


    //Not sure if this should be here on this script.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out ObstacleHookController obstacle) && playerLifeController.isAlive)
        {
            obstacle.DeactivateHook();
        }

    }

    public void DisableInput()
    {
        characterActions.Disable();
        playerInput.actions.Disable();
    }

    public void EnableInput()
    {
        characterActions.Enable();
        playerInput.actions.Enable();
    }

}
