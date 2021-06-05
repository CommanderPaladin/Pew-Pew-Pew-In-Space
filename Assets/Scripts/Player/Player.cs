using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ShipPhysics))]
[RequireComponent(typeof(ShipInput))]
public class Player : Entity
{
    public bool isPlayer = false;

    [Header("Multipliers")]
    public float rateOfFire;
    public float damage;
    public float speed;
    public float life;

    private ShipInput input;
    private ShipPhysics physics;
    
    // Keep a static reference for whether or not this is the player ship. It can be used
    // by various gameplay mechanics. Returns the player ship if possible, otherwise null.
    public static Player PlayerShip { get { return playerShip; } }
    private static Player playerShip;

    // Getters for external objects to reference things like input.
    public Vector3 Velocity { get { return physics.Rigidbody.velocity; } }
    public float Throttle { get { return input.throttle; } }

    private void Awake()
    {
        input = GetComponent<ShipInput>();
        physics = GetComponent<ShipPhysics>();
    }

    private void Update()
    {
        // Pass the input to the physics to move the ship.
        physics.SetPhysicsInput(new Vector3(input.strafe, input.updown, input.throttle), new Vector3(input.pitch, input.yaw, input.roll));

        // If this is the player ship, then set the static reference. If more than one ship
        // is set to player, then whatever happens to be the last ship to be updated will be
        // considered the player. Don't let this happen.
        if (isPlayer)
            playerShip = this;
    }
}
