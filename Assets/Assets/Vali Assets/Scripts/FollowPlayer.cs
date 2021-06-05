using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public float torque = 500f;
    public float thrust = 1000f;
    private Rigidbody rb;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(player);
        Vector3 targetLocation = player.position - transform.position;
        float distance = targetLocation.magnitude;
    
        rb.AddRelativeForce(Vector3.forward * Mathf.Clamp(((distance - 10)/50),0f,1f)*thrust);
   
    }
}
