using System.ComponentModel;
using UnityEngine;

public class PunchState2 : PlayerAttackState
{
    private float forwardImpluse = 2f;
    private bool comboNext;
    private bool hasChanged;

    public PunchState2(Player player, PlayerStateMachine stateMachine, Animator animator, string animationName)
: base(player, stateMachine, animator, animationName) { }

    public override void Enter()
    {
        base.Enter();
        comboNext = false;
        player.rb.AddForce(new Vector2(1, 0) * player.direction * forwardImpluse, ForceMode2D.Impulse);
    }
    public override void TransitionChecks()
    {
        base.TransitionChecks();
        if (player.isAttackPressed) comboNext = true;

        if (Time.time - startTime > 0.3f)
        {
            if (comboNext && !isAnimationFinished)
            {
                isAnimationFinished = true;
                stateMachine.ChangeState(player.PunchState1);
                comboNext = false;
                
            }
        }

        if(Time.time - startTime > 0.32f) stateMachine.ChangeState(player.IdleState);
    }
}
