using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    [HideInInspector] public Rigidbody2D rb;
    public PlayerStateMachine stateMachine;
    public Animator anim;
    public SpriteRenderer sprite;
    public int direction;

    //box projection for ground check
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    public float horizontalInput;
    public bool isJumpPressed;
    public bool isHoldingShift;
    public bool isAttackPressed;

    //circle projection for collision check when attacking
    public GameObject attackPoint;
    public Vector2 attackPointPosition;
    public Vector2 attackBoxSize;
    public LayerMask enemyLayer;
    public IEnemy enemyObject;

    //define player states
    public IdleState IdleState;
    public WalkState WalkState;
    public RunningState RunningState;
    public JumpState JumpState;
    public PunchState1 PunchState1;
    public PunchState2 PunchState2;

  
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
        PunchState1 = new PunchState1(this, stateMachine, anim, "Punch1");
        PunchState2 = new PunchState2(this, stateMachine, anim, "Punch2");

        stateMachine.InitializeStateMachine(IdleState);
    }


    void Update()
    {
        //take inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(!(stateMachine._currentState is PlayerAttackState))
        {
            if (horizontalInput > 0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
            }
            else if (horizontalInput < 0)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f); 
            }
        }
       

        direction = transform.rotation.y < 0f? -1 : 1;  

        isJumpPressed = Input.GetButtonDown("Jump");
        isHoldingShift = Input.GetKey(KeyCode.LeftShift);
        isAttackPressed = Input.GetMouseButtonDown(0);

        stateMachine._currentState.LogicUpdate();

        if (isJumpPressed == true && IsGrounded())
        {
            stateMachine.ChangeState(JumpState);
        }

        else if ((isAttackPressed == true && IsGrounded()) && !(stateMachine._currentState is PlayerAttackState)) {
            stateMachine.ChangeState(PunchState1);
        }

       
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

    public void Attack()
    {
        
        Collider2D[] enemy = Physics2D.OverlapBoxAll(attackPoint.transform.position, attackBoxSize, enemyLayer);

        foreach (Collider2D other in enemy) {
            if (other.TryGetComponent<IEnemy>(out var enemyObject))
            {
                enemyObject.TakeDamage(1, 10);
            }
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
        Gizmos.DrawWireCube(attackPoint.transform.position, attackBoxSize);
    }



    

}
