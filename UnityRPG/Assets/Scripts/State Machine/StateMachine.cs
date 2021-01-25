﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private  List<StateTransition> _stateTransitions = new List<StateTransition>();
    private List<StateTransition> _anyStateTransition = new List<StateTransition>();
    
    private IState _currentState;
    public IState CurrentState => _currentState;

    public void AddAnyTransition(IState to, Func<bool> condition)
    {
        var stateTransition = new StateTransition(null, to, condition);
        _anyStateTransition.Add(stateTransition);
    }
    
    public void AddTransition(IState from, IState to, Func<bool> condition)
    {
        var stateTransition = new StateTransition(from, to, condition);
        _stateTransitions.Add(stateTransition);
    }
    public void SetState(IState state)
    {
        if(_currentState == state)
            return;

        _currentState?.OnExit();
        
        _currentState = state;
        Debug.Log($"Changed to state {state}");
        _currentState.OnEnter();
    }

    public void Tick()
    {
        StateTransition transition = CheckForTransition();
        if (transition != null)
        {
            SetState(transition.To);
        }
        _currentState.Tick();
    }

    private StateTransition CheckForTransition()
    {
        foreach (var transition in _anyStateTransition)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }
        
        foreach (var transition in _stateTransitions)
        {
            if(transition.From == _currentState && transition.Condition())
                return transition;
        }
    
        return null;
    }
}