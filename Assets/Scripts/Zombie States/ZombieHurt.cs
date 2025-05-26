using UnityEngine;

public class ZombieHurt : EnemyState
{
    bool isDeath;
    public ZombieHurt(IEnemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationName) :
      base(enemy, stateMachine, animator, animationName)
    { }

    public override void Enter()
    {
        if ((enemy as Zombie).health <= 0) isDeath = true;
        base.Enter();
        enemy.Rigidbody2D.AddForce(new Vector2((enemy as Zombie).lastHitDirection, 0) * (enemy as Zombie).lastHitForce, ForceMode2D.Impulse);
    }

    public override void TransitionChecks()
    {
        if (Time.time - startTime > 0.2f) {
            if (isDeath) stateMachine.ChangeState("Death");
            else stateMachine.ChangeState("Idle");
        } 
        base.TransitionChecks();
    }
}
