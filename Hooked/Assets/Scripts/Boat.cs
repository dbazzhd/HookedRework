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

    private StateMachine statemachine = new StateMachine();
    private bool inBounds;
	// Use this for initialization
	void Start ()
    {
        Acceleration = new Vector2(interalAcceleration, internalDeceleration);
        MaxVelocity = interalMaxVelocity;
        inBounds = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        statemachine.ExecuteStateUpdate();
        if (Input.GetKeyDown(KeyCode.E))
        {
            statemachine.ChangeState(new StateTravelTo(this, statemachine, GameObject.Find("CubePoint").transform.position, true, false));
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            statemachine.ChangeState(new StateRotateAroundY(this.gameObject, statemachine, 1.0f));
        }        
        /*
         
        move: Accel then apply flat velo
        if sign(mouse pos - boat pos ) != sign 
         
         */

    }

}
