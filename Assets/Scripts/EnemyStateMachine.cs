using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    string current;
    private readonly Dictionary<string, EnemyState> _states= new Dictionary<string, EnemyState>();
    
    public EnemyState CurrentState;
    public void Register(string key, EnemyState state)
    {
        _states[key] = state;
    }
    public void ChangeState(string key)
    {
        CurrentState.Exit();
        CurrentState = _states[key];
        current = key;
        CurrentState.Enter();
    }

    public void InitializeStateMachine(string key)
    {
        CurrentState = _states[key];
        CurrentState.Enter();
    }

    public void GetState()
    {
        Debug.Log(current);
    }
}
