using UnityEngine;

public class ZombieDeath : EnemyState
{
    public ZombieDeath(IEnemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationName) :
    base(enemy, stateMachine, animator, animationName)
    { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time - startTime > 3f)
        {
            (enemy as Zombie).OnDeathAnimationComplete();
        }
    }
}
