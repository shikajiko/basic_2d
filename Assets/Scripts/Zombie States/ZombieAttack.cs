using UnityEngine;

public class ZombieAttack : EnemyState
{
    public ZombieAttack(IEnemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationName) :
   base(enemy, stateMachine, animator, animationName)
    { }

    public override void Enter()
    {
        base.Enter();
       
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
   
    }

    public override void TransitionChecks()
    {
        base.TransitionChecks();
        if(Time.time - startTime > 0.8f)
        {
            stateMachine.ChangeState("Aggro");
        }
    }
}
