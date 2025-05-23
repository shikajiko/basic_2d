using UnityEngine;

public class WalkState : PlayerState
{
    public WalkState(Player player, PlayerStateMachine stateMachine, Animator animator, string animationName)
        : base(player, stateMachine, animator, animationName) { }

    private float speed = 300f;
    public override void TransitionChecks()
    {
        base.TransitionChecks();

        if (player.isHoldingShift && player.horizontalInput != 0)
        {
            stateMachine.ChangeState(player.RunningState);
        }

        if (player.horizontalInput == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (player.isJumpPressed == true && player.IsGrounded()) {
            stateMachine.ChangeState(player.JumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.rb.linearVelocity = new Vector2(player.horizontalInput * speed * Time.fixedDeltaTime, player.rb.linearVelocityY);
    }
}
