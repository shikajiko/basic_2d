using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState _currentState;

    public void ChangeState(EnemyState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void InitializeStateMachine(EnemyState initialState)
    {
        _currentState = initialState;
        _currentState.Enter();
    }
}
