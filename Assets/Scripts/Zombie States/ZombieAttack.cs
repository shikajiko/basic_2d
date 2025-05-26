using UnityEngine;

public class ZombieAttack : EnemyState
{
    public ZombieAttack(IEnemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationName) :
   base(enemy, stateMachine, animator, animationName)
    { }

   
    private int direction;

    public override void Enter()
    {
        base.Enter();
       
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        direction = enemy.Rigidbody2D.position.x > enemy.playerPosition.x ? -1 : 1;
        if (direction > 0) enemy.Sprite.flipX = false;
        else if (direction < 0) enemy.Sprite.flipX = true;

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
