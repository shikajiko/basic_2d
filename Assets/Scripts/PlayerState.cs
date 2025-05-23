using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected Animator animator;
    protected string animationName;

    protected bool isExistingState;
    protected bool isAnimationFinished;
    protected float startTime;

    //get each necessary component from Monobehavior script
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, Animator _animator, string _animationName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animator = _animator;   
        animationName = _animationName;
    }
    //called when the player is entering a new state
    public virtual void Enter()
    {
        isAnimationFinished = false;
        isExistingState = false;
        startTime = Time.time;
        animator.SetBool(animationName, true);
    }
    //called when the player is exiting a new state
    public virtual void Exit() { 
        isExistingState = true;
        if(!isAnimationFinished) isAnimationFinished = true;
        animator.SetBool(animationName, false);
    }

    public virtual void LogicUpdate()
    {
        TransitionChecks();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void TransitionChecks()
    {

    }

    public virtual void AnimationTrigger()
    {
        isAnimationFinished = true;
    }
}
