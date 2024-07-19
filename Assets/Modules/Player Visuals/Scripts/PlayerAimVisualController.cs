using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimVisualController : MonoBehaviour
{
    [SerializeField] float divisionForLine = 50f; //not sure why I need to divide it by 50 to make it work properly... it was trial and error.

    [Header("Aim Line Smoothness/Length")]
    [SerializeField] private int segmentsCount = 50;
    [SerializeField] private float curveLength = 3.5f;
    
    private Vector2[] segments;
    private LineRenderer lineRenderer;

    [Header("Speed and Gravity")]
    [SerializeField] private PlayerMovementController playerMovementController;
    private Rigidbody2D rb; //maybe don't need.
    private float speed;
    private float gravityFromRB;

    public Vector2 aim = Vector2.zero;
    public bool lineActive = false;
    

    private const float TIME_CURVE_ADDITION = 0.5f;


    private void Awake()
    {
        playerMovementController.OnAimEvent += OnAimEvent;
    }

    private void OnDestroy()
    {
        playerMovementController.OnAimEvent -= OnAimEvent;

    }

    // Start is called before the first frame update
    private void Start()
    {
        //Initialize segments.
        segments = new Vector2[segmentsCount];

        //Grab line renderer component and set it's number of points.
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segmentsCount;

        //Grab Player Jump force and gravity.
        speed = playerMovementController.jumpForce;
        gravityFromRB = GetComponent<Rigidbody2D>().gravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        
        LineDrawer();
    }

    private void LineDrawer()
    {
        if(playerMovementController.canJump && aim != Vector2.zero)
        {
            lineActive = true;
            lineRenderer.enabled = true;
        }

        if(lineActive)
        {
            lineRenderer.enabled = true;

            //set the starting position of the line renderer
            Vector2 startPosition = gameObject.transform.position;
            segments[0] = startPosition;
            lineRenderer.SetPosition(0, startPosition);

            //calculate physics
            Vector2 startVelocity = aim.normalized * speed / divisionForLine; //maybe instead of this I need to get the velocity. I'm using aim * speed (or JumpForce) to ADDFORCE not that that's what the velocity is...

            for(int i = 1; i < segmentsCount; i++)
            {
                //Compute the time offset
                float timeOffset = i * Time.fixedDeltaTime * curveLength;

                //Compute gravity offset
                Vector2 gravityOffset = TIME_CURVE_ADDITION * Physics2D.gravity * gravityFromRB * Mathf.Pow(timeOffset,2);

                //Set the position of the point in the line renderer
                segments[i] = segments[0] + startVelocity * timeOffset + gravityOffset;
                lineRenderer.SetPosition(i, segments[i]);
    ;        }
        }

        if(!playerMovementController.canJump)
        {
            lineActive = false;
            lineRenderer.enabled = false;
        }
        

        if(-0.1 < aim.x && aim.x < 0.1 && -0.1 < aim.y && aim.y < 0.1)
        // this is just correction the issue that the joystick when let go, doesn't go exactly to 0. Mechanical problem.
        {
            lineActive = false;
            lineRenderer.enabled = false;
        }


    }

    private void OnAimEvent(InputValue value)
    {
        aim = value.Get<Vector2>();
        lineActive = true;
    }
}
