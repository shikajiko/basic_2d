using UnityEngine;

public class PunchState1 : PlayerAttackState
{
    private float forwardImpluse = 1f;
    private bool comboNext;
    public PunchState1(Player player, PlayerStateMachine stateMachine, Animator animator, string animationName)
    : base(player, stateMachine, animator, animationName) { }

    public override void Enter()
    {
        base.Enter();
        comboNext = false;
        isAnimationFinished = true;
        player.rb.AddForce(new Vector2(1, 0) * player.direction *  forwardImpluse, ForceMode2D.Impulse);
    }
    public override void TransitionChecks()
    {
        base.TransitionChecks();
        if (player.isAttackPressed) comboNext = true;

        if (Time.time - startTime > 0.3f)
        {
            if (comboNext) stateMachine.ChangeState(player.PunchState2);
            else stateMachine.ChangeState(player.IdleState);
        }
        
    }
}
