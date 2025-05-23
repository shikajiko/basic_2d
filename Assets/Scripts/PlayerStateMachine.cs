using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState _currentState;

    public void ChangeState(PlayerState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void InitializeStateMachine(PlayerState initialState)
    {
        _currentState = initialState;
        _currentState.Enter();
    }
}
