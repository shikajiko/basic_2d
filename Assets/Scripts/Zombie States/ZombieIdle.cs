using UnityEngine;

public class ZombieIdle : EnemyState
{
    public ZombieIdle(IEnemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationName) :
       base(enemy, stateMachine, animator, animationName)
    { }
}

    

