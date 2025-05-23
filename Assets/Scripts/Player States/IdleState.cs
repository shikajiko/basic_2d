using UnityEngine;

public class IdleState : PlayerState
{   
    public IdleState(Player player, PlayerStateMachine stateMachine, Animator animator, string animationName)
        : base(player, stateMachine, animator, animationName) { }

    public override void TransitionChecks()
    {
        base.TransitionChecks();

        if (player.horizontalInput != 0) {
            stateMachine.ChangeState(player.WalkState);
        }

        if (player.isJumpPressed == true && player.IsGrounded())
        {
            stateMachine.ChangeState(player.JumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.rb.linearVelocityX = Mathf.Lerp(player.rb.linearVelocityX, 0f, 0.2f);
    }

}
