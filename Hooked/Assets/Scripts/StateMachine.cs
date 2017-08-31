using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    private IState previousState;
    private IState currentState;
	public void ChangeState(IState pNewState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        previousState = currentState;
        currentState = pNewState;
        currentState.Enter();
    }

    public void ExecuteStateUpdate()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }

    public void ReturnToPreviousState()
    {
        currentState.Exit();
        currentState = previousState;
        if (currentState != null)
        {
            currentState.Enter();
        }
    }
}
