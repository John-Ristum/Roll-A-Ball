using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1;
    public float speed2;

    void Start()
    {
        //Get the Rigidbidy component of the gameObject
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Get the input value from our horizontal axis
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Get the input value from our vertical axis
        float moveVertical = Input.GetAxis("Vertical");

        //Create a new vector 3 based on the horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //Add force to the rigidbody based on our movement vector
        rb.AddForce(movement * speed);
    }
}
