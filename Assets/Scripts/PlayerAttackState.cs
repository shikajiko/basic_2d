using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, Animator animator, string animationName)
: base(player, stateMachine, animator, animationName) { }
}
