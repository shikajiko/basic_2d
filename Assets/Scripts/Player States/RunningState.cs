using UnityEngine;

public class RunningState : PlayerState
{
    public RunningState(Player player, PlayerStateMachine stateMachine, Animator animator, string animationName)
    : base(player, stateMachine, animator, animationName) { }

    private float speed = 600f;

    public override void TransitionChecks()
    {
        if (!player.isHoldingShift)
        {
            if(player.horizontalInput != 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.WalkState);
            }
        }

        if(player.IsGrounded() && player.isJumpPressed)
        {
            stateMachine.ChangeState(player.JumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.rb.linearVelocity = new Vector2(player.horizontalInput * speed * Time.fixedDeltaTime, player.rb.linearVelocityY);
    }
}
