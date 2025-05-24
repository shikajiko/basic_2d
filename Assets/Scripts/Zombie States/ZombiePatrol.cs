using UnityEngine;

public class ZombiePatrol : EnemyState
{
    private float maxPatrolDist = 20f;
    private float initialPatrolX;
    private float walkSpeed = 300f;
    private int direction;
    private float aggroTime;
    private bool hasAggro = false;
    public ZombiePatrol(IEnemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationName) : 
        base(enemy, stateMachine, animator, animationName){ }

    public override void Enter()
    {
        base.Enter();
        initialPatrolX = enemy.Rigidbody2D.position.x;
        direction = 1;
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
       

        if ((initialPatrolX - enemy.Rigidbody2D.position.x) > maxPatrolDist && direction == -1) {
            direction *= -1;
        }
        else if ((initialPatrolX - enemy.Rigidbody2D.position.x) < -maxPatrolDist && direction == 1)
        {
            direction *= -1;
        }

        enemy.Rigidbody2D.linearVelocityX = direction * walkSpeed * Time.fixedDeltaTime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (direction > 0) enemy.Sprite.flipX = false;
        else if (direction < 0) enemy.Sprite.flipX = true;
    }

    public override void TransitionChecks()
    {
        base.TransitionChecks();
        if (enemy.isAggro)
        {
            if (!hasAggro)
            {
                aggroTime = Time.time;
                (enemy as Zombie)?.ShowAlertIcon();
                hasAggro = true;
            }

            if (hasAggro)
            {
                
                if (Time.time - aggroTime > 0.5f)
                {
                    (enemy as Zombie)?.HideAlertIcon();
                    stateMachine.ChangeState("Aggro");
                }
            }



        }
    }
}
