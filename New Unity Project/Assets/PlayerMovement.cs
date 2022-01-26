//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;

    public float forwardForce = 2000f;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Hey Stinky");
        //rb.AddForce(0, 900, 500);

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (Input.GetKey("w"))
        {
            rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-1800f * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("s"))
        {
            rb.AddForce(0, 0, -forwardForce * Time.deltaTime);
        }

        if (Input.GetKey("d"))
        {
            rb.AddForce(1800f * Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown("space"))
        {
            rb.AddForce(0, 6000f * Time.deltaTime, 0);
        }
    }
}
