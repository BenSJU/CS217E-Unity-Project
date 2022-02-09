using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO Change drag, jumping feels weird because of it
//TODO change how jumping works, using vectors in tutorial is probably better https://www.youtube.com/watch?v=E5zNi_SSP_w

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody rb;

    
    public float jumpHeight = 10;
    public bool grounded;

    public float movementMultiplier = f;
    public float moveSpeed = 3f;

    float rbDrag = 6f;

    float horizontalMovement;
    float verticalMovement;

    Vector3 moveDirection;

    public int maxJumpCount = 2;
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
        MyInput();
        ControlDrag();
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;

    }

    void ControlDrag()
    {
        rb.drag = rbDrag;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        MovePlayer();

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

        if ((Input.GetKeyDown("space")) && (currJumps > 0)) //player moves upward when they press space and they have jumps remaining
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            currJumps -= 1; //subtract one jump from amount of jumps able to be done
        }

        
    }



    void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            currJumps = maxJumpCount; //reset number of jumps remaining

        }

    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;

        }

    }
}
