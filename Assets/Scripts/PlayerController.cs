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
    private bool isRunning;
    private bool isGrounded;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        isJumpPressed = Input.GetButtonDown("Jump");
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isGrounded = IsGrounded();

        if (isJumpPressed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
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
            sprite.flipX = (horizontalInput <= 0);
        }

        anim.SetBool("grounded", isGrounded);
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

}
