using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ShipPhysics))]
[RequireComponent(typeof(AIInput))]

public class ShipAI : Entity
{
    private AIInput input;
   
    private ShipPhysics physics;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<AIInput>();
        physics = GetComponent<ShipPhysics>();
    }

    // Update is called once per frame
    void Update()
    {
        physics.SetPhysicsInput(new Vector3(input.strafe, input.updown, input.throttle), new Vector3(input.pitch, input.yaw, input.roll));
    }
}
