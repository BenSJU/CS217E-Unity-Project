using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO handle slopes, try to fix jump
//mess with the second parameter in the checksphere new Vector to fix
public class PlayerMovement : MonoBehaviour
{
    float playerHeight = 1f;

    [SerializeField] Transform orientation;

    [Header("Movement")]
    public float moveSpeed = 3f;
    [SerializeField] float airMultiplier = 0.222f;
    public float movementMultiplier = 6f;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;

    [Header("Jumping")]
    public float jumpHeight = 15f;

    [Header("Drag")]
    [SerializeField] float groundDrag = 5.7f;
    [SerializeField] float airDrag = 2f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.3f;
    public bool grounded;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    public int maxJumpCount = 1;
    public int currJumps = 0;

    public Rigidbody rb;

    RaycastHit slopeHit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Hey Stinky");
        //rb.AddForce(0, 900, 500);
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {

        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        print(grounded);

        MyInput();
        ControlDrag();
        ControlSpeed();
        MovePlayer();

        if ((Input.GetKeyDown("space")) && (currJumps > 0)) //player moves upward when they press space and they have jumps remaining
        {
            Jump();
        }

        if (grounded)
        {
            currJumps = maxJumpCount;
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;

    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        currJumps -= 1;
    }

    void ControlSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift) && grounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void ControlDrag()
    {
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        

        //      if (Input.GetKey("w"))
        //    {
        //  rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        // }

        // if (Input.GetKey("a"))
        // {
        //  rb.AddForce(-1800f * Time.deltaTime, 0, 0);
        //  }

        // if (Input.GetKey("s"))
        // {
        // rb.AddForce(0, 0, -forwardForce * Time.deltaTime);
        // }

        // if (Input.GetKey("d"))
        // {
        // rb.AddForce(1800f * Time.deltaTime, 0, 0);
        // }

        

        
    }



    void MovePlayer()
    {
        if (grounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (grounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
        
    }

    //public void OnCollisionEnter(Collision collision)
    //{
     //   if (collision.gameObject.tag == "Ground")
       // {
         //   grounded = true;
           // currJumps = maxJumpCount; //reset number of jumps remaining

       // }

//    }

  //  public void OnCollisionExit(Collision collision)
    //{
     //   if (collision.gameObject.tag == "Ground")
      //  {
       //     grounded = false;

        //}

    //}
}
