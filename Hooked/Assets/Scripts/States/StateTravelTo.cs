using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTravelTo : IState
{
    private Transformable owner;
    private Vector3 target;
    private Vector3 origin;
    private StateMachine machine;
    private float halfDestination;
    private bool useLerpTrueUseFlatFalse;
    private float lerpTimer;
    private float lerpDuration;
    private bool interuptable;

    public StateTravelTo(Transformable pOwner, StateMachine pMachine, Vector3 pTarget, bool pInteruptable, bool pUseLerpTrueUseFlatFalse, float pLerpDuration = 1.0f)
    {
        owner = pOwner;
        machine = pMachine;
        target = pTarget;
        useLerpTrueUseFlatFalse = pUseLerpTrueUseFlatFalse;
        lerpDuration = pLerpDuration;
        interuptable = pInteruptable;
    }

    public void Enter()
    {
        halfDestination = (target- owner.transform.position).magnitude / 2;
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
        Vector3 differenceVector = owner.transform.position - target;
        Debug.Log("diff: " + differenceVector + " | vel: " + owner.Velocity + " | scrnPnt: " + MouseAndTouch.GetWorldPoint() + " | owner pos: " + owner.transform.position);

        if (interuptable == true && Mathf.Sign((owner.transform.position - MouseAndTouch.GetWorldPoint()).x) != Mathf.Sign(differenceVector.x))
        {
            machine.ChangeState(new StateRotateAroundY(owner.gameObject, machine, 1.0f));
        }

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
            owner.transform.position = target;
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
            owner.transform.position = Vector3.Lerp(origin, target, lerp);
        }
        else
        {
            owner.transform.position = target;
            machine.ChangeState(new StateStationary());

        }
    }

    public void Exit()
    {
        halfDestination = 0.0f;
        target = Vector3.zero;
        owner.Velocity = 0.0f;
        lerpTimer = 0.0f;
    }
}
