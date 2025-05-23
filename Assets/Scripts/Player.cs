using UnityEngine;

public class Player : MonoBehaviour
{

    [HideInInspector] public Rigidbody2D rb;
    public PlayerStateMachine stateMachine;
    public Animator anim;
    public SpriteRenderer sprite;

    //box projection for ground check
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    public float horizontalInput;
    public bool isJumpPressed;
    public bool isHoldingShift;

    //define player states
    public IdleState IdleState;
    public WalkState WalkState;
    public RunningState RunningState;
    public JumpState JumpState;

  
    void Start()
    {
        //get each needed component
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb.freezeRotation = true;

        //instantiate states
        stateMachine = new PlayerStateMachine();
        IdleState = new IdleState(this, stateMachine, anim, "Idle");
        WalkState = new WalkState(this, stateMachine, anim, "Walk");
        RunningState = new RunningState(this, stateMachine, anim, "Run");
        JumpState = new JumpState(this, stateMachine, anim, "Jump");

        stateMachine.InitializeStateMachine(IdleState);
    }


    void Update()
    {
        //take inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput > 0)
        {
            sprite.flipX = false;
        }
        else if (horizontalInput < 0) {
            sprite.flipX = true;
        }

        isJumpPressed = Input.GetButtonDown("Jump");
        isHoldingShift = Input.GetKey(KeyCode.LeftShift);

        stateMachine._currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine._currentState.PhysicsUpdate();
    }
    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0f, -transform.up, castDistance, groundLayer))
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
