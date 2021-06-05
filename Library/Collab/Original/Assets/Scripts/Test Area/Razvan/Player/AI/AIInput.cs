using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInput : MonoBehaviour
{
    [Range(-1, 1)]
    public float pitch;
    [Range(-1, 1)]
    public float yaw;
    [Range(-1, 1)]
    public float updown;
    [Range(-1, 1)]
    public float roll;
    [Range(-1, 1)]
    public float strafe;
    [Range(0, 1)]
    public float throttle;

    // How quickly the throttle reacts to input.
    private const float THROTTLE_SPEED = 0.5f;

    // Keep a reference to the ship this is attached to just in case.


    private GameObject player;
    
    //DNP1
    private readonly VectorPid angularVelocityController = new VectorPid(33.7766f, 0, 0.2553191f);
    private readonly VectorPid headingController = new VectorPid(9.244681f, 0, 0.06382979f);
    //DPN2


    public Transform target;
    private Rigidbody rigidbody;

    private void Start()
    {
        player = GameObject.Find("Player");
        target = player.transform;
        rigidbody = this.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (player != null)
        {
            //DPN1();
            //LookAtPlayer();
            //Follow Player
            //Shot Player

            //A se folosi daca nu rezolvam cum trebuie
            //AIFailSafe();
        }

    }
   
    private void LookAtPlayer()
    {
        
    }
    private Vector3 Vector3Abs(Vector3 v1, Vector3 v2)
    {
        Vector3 temp;

        temp.x = Mathf.Abs(v1.x-v2.x);
        temp.y = Mathf.Abs(v1.y - v2.y);
        temp.z = Mathf.Abs(v1.z - v2.z);

        return temp;
    }

    private Vector3 Vector3AbsVal(Vector3 v)
    {
        v.x = Mathf.Abs(v.x);
        v.y = Mathf.Abs(v.y);
        v.z = Mathf.Abs(v.z);
        return v;
    }
    private void DPN1()
    {
        var angularVelocityError = rigidbody.angularVelocity * -1;
        Debug.DrawRay(transform.position, rigidbody.angularVelocity * 10, Color.black);
        var angularVelocityCorrection = angularVelocityController.UpdateS(angularVelocityError, Time.deltaTime);
        Debug.DrawRay(transform.position, angularVelocityCorrection, Color.green);
        var desiredHeading = target.position - transform.position;
        Debug.DrawRay(transform.position, desiredHeading, Color.magenta);
        var currentHeading = transform.forward;


        Debug.DrawRay(transform.position, currentHeading * 15, Color.blue);
        var headingError =   Vector3.Cross(currentHeading, desiredHeading);
        var headingCorrection = headingController.UpdateS(headingError, Time.deltaTime);
        Debug.Log(headingCorrection);

        if (player.transform.position.z < transform.position.z)
            pitch = -headingCorrection.x;
        else
            pitch = headingCorrection.x;

        yaw = headingCorrection.y;
        //roll = headingCorrection.z;
        //rigidbody.AddTorque(headingCorrection);
    }

    //End Test
    private void AIFailSafe()
    {
        //CEA MAI BASIC METODA DE AI
        //A SE FOLOSI DOAR IN CAZ DE URGENTA
        float rotSpeed = 3f;
        float moveSpeed = 10f;
        transform.rotation = Quaternion.Slerp(transform.rotation
                                      , Quaternion.LookRotation(player.transform.position - transform.position)
                                      , rotSpeed * Time.deltaTime);
        if (Vector3.Distance(player.transform.position,this.transform.position) > 40)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        

    }
    private void UpdatePitch(float val)
    {
        pitch += val;
        if (pitch > 1)
            pitch = 1;
        if (pitch < -1)
            pitch = -1;
    }
    private void UpdateYaw(float val)
    {
        yaw += val;
        if (yaw > 1)
            yaw = 1;
        if (yaw < -1)
            yaw = -1;
    }
}

//Collision Boom
/* Vector3 m_vHeading = this.gameObject.GetComponent<Rigidbody>().velocity;
 Vector3 toTarget = (player.transform.position - gameObject.transform.position);

 Vector3 x = Vector3.Cross(m_vHeading.normalized, toTarget.normalized);
 float angle = Mathf.Asin(x.magnitude);
 Vector3 w = x.normalized * angle / Time.fixedDeltaTime;

 Quaternion q = gameObject.transform.rotation * this.gameObject.GetComponent<Rigidbody>().inertiaTensorRotation;
 Vector3 T = q * Vector3.Scale(this.gameObject.GetComponent<Rigidbody>().inertiaTensor, (Quaternion.Inverse(q) * w));

 pitch = T.x;
 yaw = T.y;
 roll = T.z;*/
