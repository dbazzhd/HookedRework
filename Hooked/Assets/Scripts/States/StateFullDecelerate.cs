using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFullDecelerate : IState {

    private Transformable owner;
    private StateMachine machine;
    public StateFullDecelerate(Transformable pOwner, StateMachine pMachine)
    {
        owner = pOwner;
        machine = pMachine;
    }

    public void Enter()
    {

    }

    public void Execute()
    {
        if (owner.Velocity > 0)
        {
            owner.Velocity -= owner.Acceleration.y;
            if (owner.Velocity < 0) owner.Velocity = 0;
        }
        else if (owner.Velocity < 0)
        {
            owner.Velocity += owner.Acceleration.y;
            if (owner.Velocity > 0) owner.Velocity = 0;
        }
        if (owner.Velocity == 0) { machine.ReturnToPreviousState(); }
    }

    public void Exit()
    {

    }
}
