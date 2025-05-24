using UnityEngine;

public class ZombieAggro : EnemyState
{
    public ZombieAggro(IEnemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationName) :
       base(enemy, stateMachine, animator, animationName)
    { }

    private int direction;
    private float distanceToPlayer;
    private float walkSpeed = 600f;
    public override void Enter()
    {
        base.Enter();
        distanceToPlayer = Mathf.Abs(enemy.Rigidbody2D.position.x - enemy.playerPosition.x);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        direction = enemy.Rigidbody2D.position.x > enemy.playerPosition.x ? -1 : 1;
        enemy.Rigidbody2D.linearVelocityX = direction * walkSpeed * Time.fixedDeltaTime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (direction > 0) enemy.Sprite.flipX = false;
        else if (direction < 0) enemy.Sprite.flipX = true;
        distanceToPlayer = Mathf.Abs(enemy.Rigidbody2D.position.x - enemy.playerPosition.x);
    }

    public override void TransitionChecks()
    {
        base.TransitionChecks();
        if (distanceToPlayer < 2f)
        {
            stateMachine.ChangeState("Attack");
        }
    }

    
}
