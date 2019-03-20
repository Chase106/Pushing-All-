using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed = 6.0f;

    private CharacterController charaControl;
    public CapsuleCollider col;
    public LayerMask groundLayers;
    //private CharacterController player;

    private float vertVelocity;
    public float jumpForce = 4f;
    private bool hasJumped;
    // Start is called before the first frame update
    void Start()
    {
        charaControl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, vertVelocity, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        charaControl.Move(movement);
        if (Input.GetKey("space"))
        {
            Jump();
            hasJumped = true;
        }
        ApplyGravity();
    }

     private void Jump()
    {
        vertVelocity = jumpForce;
    }

    private void ApplyGravity()
    {
        if (charaControl.isGrounded == true)
        {
            if (hasJumped == false)
            {
                vertVelocity = 0;
            }
            else
            {
                vertVelocity = jumpForce;
            }
        }
        else
        {
            vertVelocity += Physics.gravity.y * Time.deltaTime;
            vertVelocity = Mathf.Clamp(vertVelocity, -50f, jumpForce);
            hasJumped = false;
        }
    }
}
