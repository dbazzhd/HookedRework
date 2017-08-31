using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Transformable {

    [SerializeField]
    private float interalAcceleration;
    [SerializeField]
    private float interalMaxVelocity;
    [SerializeField]
    private float internalDeceleration;
    [SerializeField]
    private float internalVelocity;

    private StateMachine statemachine = new StateMachine();
    private bool inBounds;
	// Use this for initialization
	void Start ()
    {
        Acceleration = new Vector2(interalAcceleration, internalDeceleration);
        MaxVelocity = interalMaxVelocity;
        Velocity = internalVelocity;
        inBounds = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        statemachine.ExecuteStateUpdate();
	}

}
