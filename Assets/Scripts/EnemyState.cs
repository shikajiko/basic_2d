using UnityEngine;

public class EnemyState
{
    protected IEnemy enemy;
    protected EnemyStateMachine stateMachine;
    protected Animator animator;
    protected string animationName;

    protected bool isAnimationFinished;
    protected float startTime;

    public EnemyState(IEnemy enemy, EnemyStateMachine stateMachine, Animator animator, string animationName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animator = animator;
        this.animationName = animationName;
    }

    public virtual void Enter()
    {
        isAnimationFinished = false;
        startTime = Time.time;
        animator.SetBool(animationName, true);
    }

    public virtual void Exit()
    {
        isAnimationFinished = true;
        animator.SetBool(animationName, false);
    }

    public virtual void LogicUpdate()
    {
        TransitionChecks();
    }

    public virtual void PhysicsUpdate() { }

    public virtual void TransitionChecks() { }

    public virtual void AnimationTrigger()
    {
        isAnimationFinished = true;
    }
}
