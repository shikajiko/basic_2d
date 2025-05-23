using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 20.0f;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    public float dampCoefficient = 0.5f;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private float horizontalInput;
    private bool isJumpPressed;
    private bool hasJumped = false;
    private bool isRunning = false;
    private bool isGrounded;
    private bool canMove = true;

    void Start()
    {
        // get components
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        //freeze rotation to make sure the character doesn't fall when applied force
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (canMove)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            isJumpPressed = Input.GetButtonDown("Jump");
            isRunning = Input.GetKey(KeyCode.LeftShift);
        }

        else
        {
            horizontalInput = 0;
            isJumpPressed = false;
            isRunning = false; 
        }

        if (isJumpPressed && isGrounded)
        {
            anim.SetTrigger("jump");
            hasJumped = true;
            isGrounded = false;
            StartCoroutine(JumpRoutine());
        }

        if (!hasJumped)
        {
            isGrounded = IsGrounded();
            anim.SetBool("grounded", isGrounded);
        }

        if (hasJumped && rb.linearVelocityY <= -0.01f && IsGrounded())
        {
            hasJumped = false;
            anim.SetBool("grounded", isGrounded);
        }

        if (horizontalInput == 0)
        {
            anim.SetFloat("speed", 0);
        }
        else 
        {
            if (!isRunning)
            {
                anim.SetFloat("speed", 0.4f);
            }
            else
            {
                anim.SetFloat("speed", 1);
            }
            sprite.flipX = (horizontalInput < 0);
        }

        anim.SetBool("isJumping", hasJumped);
    }

    private void FixedUpdate()
    {
        if (horizontalInput != 0)
        {
            if (isRunning) rb.linearVelocity = new Vector2(horizontalInput * speed * 3 * Time.fixedDeltaTime, rb.linearVelocityY);
            else rb.linearVelocity = new Vector2(horizontalInput * speed * Time.fixedDeltaTime, rb.linearVelocityY);
        }
        if (!isGrounded || isRunning)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, new Vector2(0f , rb.linearVelocityY), dampCoefficient);
        }
            
        
    }

    private bool IsGrounded()
    {
        if(Physics2D.BoxCast(transform.position, boxSize, 0f, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        return false; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    private IEnumerator JumpRoutine()
    {
        canMove = false;
        yield return new WaitForSeconds(0.1f);
        Vector2 upForce = Vector2.up * jumpForce;
        rb.AddForce(upForce, ForceMode2D.Impulse);
        anim.SetBool("isJumping", true);
        canMove = true;

    }

}
