using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private bool toRun;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, Animator animator, string animationName)
: base(player, stateMachine, animator, animationName) { }

    public override void Enter()
    {
        base.Enter();
        if (player.horizontalInput != 0 && player.isHoldingShift) toRun = true;
    }
   
}
