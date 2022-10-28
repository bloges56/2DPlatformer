using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    Rigidbody2D rb;
    [SerializeField]
    float speed;
    [SerializeField]
    float airSpeed;
    [SerializeField]
    KeyCode jumpKey = KeyCode.Space;
    bool readyToJump;
    bool grounded;
    [SerializeField]
    LayerMask whatIsGround;
    float horizontalInput;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float playerHeight;
    [SerializeField]
    float jumpCooldown;
    [SerializeField]
    float groundDrag;

    Animator anim;
    SpriteRenderer render;

    public bool win;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        readyToJump = Physics2D.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Goal")
        {
            win = true;
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if(horizontalInput > 0 )
        {
            render.flipX = false;
        }
        else if(horizontalInput < 0)
        {
            render.flipX = true;
        }

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {  
            //make player jump
            readyToJump = true;
            Jump();

            //holder enter will continously jump
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void SpeedControl()
    {
        //get the velocity in x and y
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, 0f);

        //check if the velocity magnitude is greater than speed
        if(flatVel.magnitude > speed)
        {
            //get direction of flat velocity with the magnitude of max speed
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void MovePlayer()
    {
        //determine direction to move
        Vector3 moveDirection = transform.right * horizontalInput;

        if(grounded)
        {
            //add force in move direction
            rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode2D.Force);
        }
        else
        {
            //add force in move direction
            rb.AddForce(moveDirection.normalized * airSpeed * 10f, ForceMode2D.Force);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));
        if(rb.velocity.y < 0)
        {
            anim.SetBool("IsFalling", true);
            anim.SetBool("IsJumping", false);
        }
        else if(rb.velocity.y > 0)
        {
            anim.SetBool("IsFalling", false);
            anim.SetBool("IsJumping", true);
        }
        else
        {
             anim.SetBool("IsFalling", false);
             anim.SetBool("IsJumping", false);
        }
        MyInput();

         if(grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void Jump()
    {
        //reset vertical velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);

        //add force in vertical direction
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    void FixedUpdate() {
        MovePlayer();
    }
}
