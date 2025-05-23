using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(Player player, PlayerStateMachine stateMachine, Animator animator, string animationName)
     : base(player, stateMachine, animator, animationName) { }

    private bool isGrounded = false;
    private bool hasJumped = false;
    private float jumpDelay = 0.1f;
    private float jumpForce = 40.0f;
    private float flySpeed = 600f;
   
    public override void Enter()
    {
        base.Enter();

        hasJumped = false;
        isGrounded = false;
        flySpeed = 600f;

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (hasJumped && player.rb.linearVelocityY < 0)
        {
            isAnimationFinished = true;
        }
        
        if(isAnimationFinished) isGrounded = player.IsGrounded();
    }

    public override void TransitionChecks()
    {
        base.TransitionChecks();
        if (base.isAnimationFinished && isGrounded) {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!hasJumped) {
            flySpeed = player.isHoldingShift ? 900f : 600f;
        }

        if (!hasJumped && Time.time - startTime > jumpDelay) {
            Vector2 upForce = Vector2.up * jumpForce;
            player.rb.AddForce(upForce, ForceMode2D.Impulse);
            hasJumped = true;
        }

        if(player.horizontalInput != 0 && hasJumped)
        {
            player.rb.linearVelocityX = Mathf.Lerp(player.horizontalInput * flySpeed * Time.fixedDeltaTime, 0f, 0.3f);
        }
    }
}
