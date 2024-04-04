using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public Animator animator;

    public float speed;
    public float crouchSpeed;
    public float input;
    public SpriteRenderer spriteRenderer;
    public float jumpForce;

    public LayerMask groundlayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;

    public float jumpTime = 0.35f;
    public float jumpTimeCounter;

    private bool isJumping;
    private bool isCrouching;


    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        input = Input.GetAxisRaw("Horizontal");
        Debug.Log(input);
        if (input < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (input > 0)
        {
            spriteRenderer.flipX = false;
        }

        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundlayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            playerRB.velocity = Vector2.up * jumpForce;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                playerRB.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                animator.SetBool("IsJumping", false);
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("IsCrouching", true);
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("IsCrouching", false);
            isCrouching = false;
        }
    }
    

    void FixedUpdate()
    {
        float currentSpeed = isCrouching ? crouchSpeed : speed;
        playerRB.velocity = new Vector2(input * currentSpeed, playerRB.velocity.y);
    }
}
