using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRotateAroundY : IState {

    private GameObject owner;
    private StateMachine machine;
    private Quaternion targetQua;
    private Quaternion originQua;
    private float rotationDuration;
    private float rotationTimer;

    public StateRotateAroundY(GameObject pOwner, StateMachine pMachine, float pDuration)
    {
        owner = pOwner;
        machine = pMachine;
        rotationDuration = pDuration; 
    }

    public void Enter()
    {
        originQua = owner.transform.rotation;
        targetQua = originQua * Quaternion.AngleAxis(180, Vector3.up);
        rotationTimer = 0.0f;
    }

    public void Execute()
    {
        rotationTimer += Time.deltaTime;
        if (rotationTimer <= rotationDuration)
        {
            float lerp = rotationTimer / rotationDuration;
            owner.transform.rotation = Quaternion.Lerp(originQua, targetQua, lerp);
        }
        else
        {
            owner.transform.rotation = targetQua;
            machine.ReturnToPreviousState();
        }
    }

    public void Exit()
    {

    }
}
