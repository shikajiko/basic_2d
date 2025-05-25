using UnityEngine;

public class RunningState : PlayerState
{
    public RunningState(Player player, PlayerStateMachine stateMachine, Animator animator, string animationName)
    : base(player, stateMachine, animator, animationName) { }

    private float speed = 600f;

    public override void TransitionChecks()
    {
        if(Mathf.Abs(player.horizontalInput) < 1)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (!player.isHoldingShift)
        {
            stateMachine.ChangeState(player.WalkState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.rb.linearVelocity = new Vector2(player.horizontalInput * speed * Time.fixedDeltaTime, player.rb.linearVelocityY);
    }
}
