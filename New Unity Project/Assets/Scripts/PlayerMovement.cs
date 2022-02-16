using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO handle slopes, try to fix jump
//mess with the second parameter in the checksphere new Vector to fix
public class PlayerMovement : MonoBehaviour
{
    //float playerheight = 2f;

    [SerializeField] Transform orientation;

    [Header("Movement")]
    public Rigidbody rb;

    public float jumpHeight = 15f;
    public bool grounded;
    float groundDistance = 0.000000000000000000000001f;
    [SerializeField] LayerMask groundMask;

    public float movementMultiplier = 6f;
    public float moveSpeed = 3f;

    [SerializeField] float airMultiplier = 0.222f;

    [Header("Drag")]
    float groundDrag = 5.7f;
    float airDrag = 1.65f;

    float horizontalMovement;
    float verticalMovement;

    Vector3 moveDirection;

    public int maxJumpCount = 1;
    public int currJumps = 0;

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

        grounded = Physics.CheckSphere(transform.position - new Vector3(0, 0.04f, 0), groundDistance, groundMask);

        print(grounded);

        MyInput();
        ControlDrag();
        MovePlayer();

        if ((Input.GetKeyDown("space")) && (currJumps > 0)) //player moves upward when they press space and they have jumps remaining
        {
            rb.drag = airDrag;
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            currJumps -= 1; //subtract one jump from amount of jumps able to be done
        }

        if (grounded)
        {
            currJumps = maxJumpCount;
        }
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;

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
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else
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
