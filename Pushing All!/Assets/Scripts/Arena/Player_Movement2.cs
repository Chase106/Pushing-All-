using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Movement2 : MonoBehaviour
{
    public float mouseSens = 3f;
    public float moveSpeed = 3f;
    public GameObject eyes;
    private CharacterController player;
    private float moveFB, moveLR, rotX, rotY;

    public float minimumVert = -65.0f;
    public float maximumVert = 45.0f;
    private float vertVelocity;
    public float jumpForce = 4f;
    private bool hasJumped;
    private bool hasDoubleJumped;
    public int JumpCount;
    private float NoForce = 0;
    public float RunSpeed = 4f;
    private float realTimeCount = 1;

    public Animator playerAnimator;
    public bool AbleToShoot = true;

    /// </Dash Function>
    public Vector3 moveDirection;
    public float maxDashTime = 1.0f;
    public float dashSpeed = 1000.0f;
    public float dashStoppingSpeed = 0.1f;
    private float currentDashTime;
    public bool isDashed = false;
    /// </Dash Function>
    /// 
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GetComponent<CharacterController>();
        playerAnimator.SetTrigger("Draw");
        currentDashTime = maxDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetKey("escape"))
        {
            //Cursor.lockState = CursorLockMode.None;
        }
        if (JumpCount >= 2 && player.isGrounded == false)
        {
            jumpForce = NoForce;

        }
        if (player.isGrounded == true)
        {
            JumpCount = 0;
            jumpForce = 8;
            hasDoubleJumped = true;
            playerAnimator.SetBool("Idle", true);
            isDashed = false;

        }
        if (Input.GetKeyDown(KeyCode.Q) && isDashed == false && player.isGrounded == false)
        {
            currentDashTime = 0.0f;
        }
        if (currentDashTime < maxDashTime)
        {
            moveDirection = transform.forward * dashSpeed;
            currentDashTime += dashStoppingSpeed;
            isDashed = true;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
        player.Move(moveDirection * Time.deltaTime);

        if (transform.position.y < -100)
        {
            SceneManager.LoadScene("TitleScreen");
        }

    }

    void Movement()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = RunSpeed;
            playerAnimator.SetFloat("Run", 0.2f);
            AbleToShoot = false;
        } 
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 3f;
            playerAnimator.SetFloat("Run", 0f);
            AbleToShoot = true;
        }
        
        moveFB = Input.GetAxis("Vertical") * moveSpeed;
        moveLR = Input.GetAxis("Horizontal") * moveSpeed;
        
        rotX = Input.GetAxis("Mouse X") * mouseSens;
        rotY -= Input.GetAxis("Mouse Y") * mouseSens;
        rotY = Mathf.Clamp(rotY, minimumVert, maximumVert);
        Vector3 movement = new Vector3(moveLR, vertVelocity, moveFB);
        transform.Rotate(0, rotX, 0);
        movement = transform.rotation * movement;
        eyes.transform.localRotation = Quaternion.Euler(rotY, 0, 0);


       



        player.Move(movement*Time.deltaTime);

        

        if (Input.GetKeyDown("space") && hasDoubleJumped == true)
        {
            Jump();
            hasJumped = true;
            JumpCount++;
           
        }
        ApplyGravity();
    }

    private void Jump()
    {
        vertVelocity = jumpForce;
    }

    private void ApplyGravity()
    {
        if (player.isGrounded == true)
        {
            if (hasJumped == false)
            {
                vertVelocity = Physics.gravity.y;
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

        if (player.isGrounded == false && JumpCount >= 2)
        {
            hasDoubleJumped = false;
            playerAnimator.SetBool("Idle", true);
        }
    }
}
