using UnityEngine;

public class PunchState2 : PlayerAttackState
{
    private float forwardImpluse = 2f;
    public PunchState2(Player player, PlayerStateMachine stateMachine, Animator animator, string animationName)
: base(player, stateMachine, animator, animationName) { }

    public override void Enter()
    {
        base.Enter();
        player.rb.AddForce(new Vector2(1, 0) * player.direction * forwardImpluse, ForceMode2D.Impulse);
    }
    public override void TransitionChecks()
    {
        base.TransitionChecks();
        if(Time.time - startTime > 0.2f)
        {
            if (player.isAttackPressed == true) stateMachine.ChangeState(player.PunchState1);
            player.isAttackPressed = false;
            
        }

        if(Time.time - startTime > 0.4f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
