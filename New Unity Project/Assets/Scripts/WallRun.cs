using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Transform orientation;

    [Header("Detection")]
    [SerializeField] float wallDistance = .5f;
    [SerializeField] float minimumJumpHeight = 1.5f;


    [Header("Wall Running")]
    [SerializeField] private float wallRunGravity;
    [SerializeField] private float wallRunJumpForce;

    public bool wallLeft = false;
    public bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    

    private Rigidbody rb;
    
    bool CanWallRun()
    {
        return !(Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight)) && CantEnemyWallRun();
    }

    bool CantEnemyWallRun()
    {

        //Raycast to check if the object being wallrun against is an enemy
        Ray ray = new Ray(transform.position, Vector3.up);
        //RaycastHit hit;

        if (wallLeft)
        {
            if ((leftWallHit.transform.gameObject.CompareTag("Enemy"))) //|| (rightWallHit.transform.gameObject.CompareTag("Enemy")))
            {
                return false;
            }
        }
        else if (wallRight)
        {
            if (rightWallHit.transform.gameObject.CompareTag("Enemy"))
            {
                return false;
            }
        }
        return true;
    }



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);

        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);
    }

    public void Update()
    {
        CheckWall();

        if (CanWallRun())
        {
            if (wallLeft)
            {
                StartWallRun();
                Debug.Log("wall running on the left");
            }
            else if (wallRight)
            {
                StartWallRun();
                Debug.Log("wall running on the right");
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    void StartWallRun()
    {
        rb.useGravity = false;

        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wallLeft)
            {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce, ForceMode.Force);
            }
            else if (wallRight)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce, ForceMode.Force);
            }
        }
    }

    void StopWallRun()
    {
        rb.useGravity = true;
    }
}
