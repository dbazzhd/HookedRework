using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformable : MonoBehaviour
{
    protected Vector2 acceleration = Vector2.zero;
    protected float maxVelocity = 0.0f;
    protected float velocity = 0.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual Vector2 Acceleration
    {
        get { return acceleration; }
        set { acceleration = value; }
    }

    public virtual float MaxVelocity
    {
        get { return maxVelocity; }
        set { maxVelocity = value; }
    }

    public virtual float Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }
}
