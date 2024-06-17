using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{

    [SerializeField] float jumpForce = 350f;

    private Vector2 aim;
    private Rigidbody2D rb;
    private UnityEngine.InputSystem.Pointer mousePosition;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

/*
    private void UpdateAim()
    {
        Vector2 playerPosition = new Vector2 (transform.position.x, transform.position.y);
        aim = playerPosition - mousePosition.position;
    }
*/
    private void OnJump(InputValue value)
    {
        if (aim == Vector2.zero)
        {
            aim.y = 1f;
        }
        rb.AddForce(aim * jumpForce);
    }
}
