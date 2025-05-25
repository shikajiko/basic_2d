using UnityEngine;

public class PunchState1 : PlayerAttackState
{
    private float forwardImpluse = 1f;
    public PunchState1(Player player, PlayerStateMachine stateMachine, Animator animator, string animationName)
    : base(player, stateMachine, animator, animationName) { }

    public override void Enter()
    {
        base.Enter();
        isAnimationFinished = true;
        player.rb.AddForce(new Vector2(1, 0) * player.direction *  forwardImpluse, ForceMode2D.Impulse);
    }
    public override void TransitionChecks()
    {
        base.TransitionChecks();
        if (Time.time - startTime > 0.2f)
        {
            if (player.isAttackPressed) stateMachine.ChangeState(player.PunchState2);
        }

        if(Time.time - startTime > 0.6f) stateMachine.ChangeState(player.IdleState);
        
    }
}
