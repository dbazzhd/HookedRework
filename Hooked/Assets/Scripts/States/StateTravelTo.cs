using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTravelTo : IState
{
    private Transformable owner;
    private Transform target;
    private Vector3 origin;
    private StateMachine machine;
    private float halfDestination;
    private bool useLerpTrueUseFlatFalse;
    private float lerpTimer;
    private float lerpDuration;

    public StateTravelTo(Transformable pOwner, StateMachine pMachine, Transform pTarget, bool pUseLerpTrueUseFlatFalse, float pLerpDuration = 1.0f)
    {
        owner = pOwner;
        machine = pMachine;
        target = pTarget;
        useLerpTrueUseFlatFalse = pUseLerpTrueUseFlatFalse;
        lerpDuration = pLerpDuration;
    }

    public void Enter()
    {
        halfDestination = (target.position - owner.transform.position).magnitude / 2;
        origin = owner.transform.position;
        lerpTimer = 0.0f;
    }

    public void Execute()
    {
        if (useLerpTrueUseFlatFalse == true)
        {
            lerpMovement();
        }
        else
        {
            flatMovement();
        }
    }

    private void flatMovement()
    {
        Vector3 differenceVector = target.position - owner.transform.position;
        if (differenceVector.magnitude > halfDestination)
        {
            owner.Velocity += owner.Acceleration.x;
            if (owner.Velocity >= owner.MaxVelocity) owner.Velocity = owner.MaxVelocity;
        }
        else if (differenceVector.magnitude <= halfDestination)
        {
            owner.Velocity -= owner.Acceleration.y;
            if (owner.Velocity < 0) owner.Velocity = 0;
        }
        if (differenceVector.magnitude < owner.Acceleration.y || owner.Velocity == 0)
        {
            owner.transform.position = target.position;
            machine.ChangeState(new StateStationary());
        }
        owner.gameObject.transform.Translate(differenceVector.normalized * owner.Velocity);
    }

    private void lerpMovement()
    {
        lerpTimer += Time.deltaTime;
        if (lerpTimer <= lerpDuration)
        {
            float lerp = lerpTimer / lerpDuration;
            owner.transform.position = Vector3.Lerp(origin, target.position, lerp);
        }
        else
        {
            owner.transform.position = target.position;
            machine.ChangeState(new StateStationary());

        }
    }

    public void Exit()
    {
        halfDestination = 0.0f;
        target = null;
        owner.Velocity = 0.0f;
        lerpTimer = 0.0f;
    }
}
