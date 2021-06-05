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

    private GameObject target;
    private Rigidbody rigidBody;
    private Transform targetTransform;
    private float distanceToPlayer;
    private bool patrolTimer = false;
    private bool blocked = false;

    public float searchingRange = 400;

    //DNP1
    //private readonly VectorPid angularVelocityController = new VectorPid(33.7766f, 0, 0.2553191f);
    //private readonly VectorPid headingController = new VectorPid(9.244681f, 0, 0.06382979f);


    //private readonly VectorPid angularVelocityController = new VectorPid(0.5f, 0.5f, 0.05f); //viteza
    //private readonly VectorPid headingController = new VectorPid(0.5f, 0.2f, 0f);    //eroare - precizie
    // Start is called before the first frame update
    private IEnumerator ExitCollisionImpact()
    {
        yield return new WaitForSeconds(0.8f);
        blocked = false;
    }
    private IEnumerator Patrol()
    {
        UpdatePitch(Random.Range(-0.4f, 0.4f));
        UpdateYaw(Random.Range(-0.4f, 0.4f));
        UpdateThrottle(Random.Range(0.2f, 1f));
        yield return new WaitForSeconds(10);
        patrolTimer = false;
    }
    private IEnumerator FindClosestEnemy()
    {
        //GameObject[] all = GameObject.FindGameObjectsWithTag("Untagged");
        Entity[] all = GameObject.FindObjectsOfType<Entity>();
        //Debug.Log(all.Length);
        GameObject targetObj = null;
        float dist = int.MaxValue;
        for (int i = 0; i < all.Length;i++)
        {
            float newD = Vector3.Distance(transform.position, all[i].gameObject.transform.position);
            if (this.gameObject!=all[i].gameObject && newD<searchingRange&& all[i].isShip==true&& this.GetComponent<Entity>().team != all[i].team && (newD<dist))
            {
                dist = newD;
                targetObj = all[i].gameObject;
            }
        }
        if (targetObj != null)
        {
            target = targetObj;
            targetTransform = target.transform;
        }

        yield return null;
    }

    private void Start()
    {
        //target = GameObject.Find("Player");
        //targetTransform = target.transform;
        StartCoroutine(FindClosestEnemy());
        rigidBody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (target != null)
        {
            if (blocked == false)
            {
                //DPN1();
                RaycastHit ray = Ray();
                if (ray.collider != null)
                {
                    Entity et = ray.collider.gameObject.transform.GetComponentInParent<Entity>();
                    if (et != null && /*et.team == 0 &&*/ Vector3.Distance(et.gameObject.transform.position, this.transform.position) < 100)
                    {
                        UpdatePitch(Random.Range(-1,1)>=0 ? 1: -1);
                        UpdateYaw(Random.Range(-1, 1) >= 0 ? 1 : -1);
                        blocked = true;
                        StartCoroutine(ExitCollisionImpact());
                    }
                    else
                    {
                        LookAtPlayer();
                    }
                }
                else
                {
                    LookAtPlayer();
                }
                //A se folosi daca nu rezolvam cum trebuie
                //AIFailSafe();
            }

        }
        else
        {
            StartCoroutine(FindClosestEnemy());
            if (target!=null)
            {
                //Nice
            }
            else
            {
                //Patrol
                if (patrolTimer==false)
                {
                    patrolTimer = true;
                    StartCoroutine(Patrol());
                }

            }
        }
    }

    public RaycastHit Ray()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 1000))
        {
            //print(hitInfo.collider.gameObject.name);
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green);
        }
        return hitInfo;
    }
    // The functions return -1 when the target direction is left, +1 when it is right and 0 if the direction is straight ahead or behind.
    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        return dir;
    }
    // The function returns 0 if perpendicular, >0 if above, <0 if below
    float UpDown(Vector3 targetDir, Vector3 up)
    {
        return Vector3.Dot(up, targetDir);
    }
    private void LookAtPlayer()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = targetTransform.position - transform.position;
        float facing = Vector3.Dot(toOther, transform.forward);
        float leftright = AngleDir(transform.forward, toOther, transform.up);
        float updown = UpDown(toOther, transform.up);
        
        float d = distanceBetween3DPoints();
        distanceToPlayer = d;
        /*if (facing < 0)
            Debug.Log("The object is behind");
        if (facing > 0)
            Debug.Log("The object is in front");
        if (updown < 0)
            Debug.Log("The object is below");
        if (updown > 0)
            Debug.Log("The object is above");
        if (leftright < 0)
            Debug.Log("The object is to the left");
        if (leftright > 0)
            Debug.Log("The object is to the right");*/

        if (Mathf.Abs(leftright) > 0.05f)
            UpdateYaw(leftright / 5);
        else
            UpdateYaw(0.0f);
        if (Mathf.Abs(updown) > 0.1f)
            UpdatePitch(-updown / 5);
        else
            UpdatePitch(0.0f);

        if (d > 60f)
        {
            UpdateThrottle(0.1f);
        }
        else 
        {
            //DE AICI SCAD VITEZA
            //UpdateThrottle(-0.1f);
        }
              
    }

    private float distanceBetween3DPoints()
    {
        float x = targetTransform.transform.position.x - this.transform.position.x;
        float y = targetTransform.transform.position.y - this.transform.position.y;
        float z = targetTransform.transform.position.z - this.transform.position.z;
        float dist = Mathf.Sqrt(x*x + y*y + z*z);
        return dist;
    }
    //End Test
    private void AIFailSafe()
    {
        //CEA MAI BASIC METODA DE AI
        //A SE FOLOSI DOAR IN CAZ DE URGENTA
        float rotSpeed = 3f;
        float moveSpeed = 10f;
        transform.rotation = Quaternion.Slerp(transform.rotation
                                      , Quaternion.LookRotation(targetTransform.transform.position - transform.position)
                                      , rotSpeed * Time.deltaTime);
        if (Vector3.Distance(targetTransform.transform.position, this.transform.position) > 40)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

    }

    private void UpdateThrottle(float val)
    {
        throttle += val;
        if (throttle > 0.6f)
            throttle = 0.6f;
        if (throttle < 0)
            throttle = 0;
    }
    private void UpdatePitch(float val)
    {
        pitch = val;
        if (pitch > 1)
            pitch = 1;
        if (pitch < -1)
            pitch = -1;
    }
    private void UpdateYaw(float val)
    {
        yaw = val;
        if (yaw > 1)
            yaw = 1;
        if (yaw < -1)
            yaw = -1;
    }
    private Vector3 Vector3Abs(Vector3 v1, Vector3 v2)
    {
        Vector3 temp;

        temp.x = Mathf.Abs(v1.x - v2.x);
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
        Debug.DrawRay(transform.position, targetTransform.position - transform.position, Color.magenta);
        Debug.DrawRay(transform.position, transform.forward * 15, Color.blue);

        Vector3 heading = Vector3.Cross(transform.forward, targetTransform.position - transform.position);

        //Vector3 headingCorrection = headingController.UpdateS(headingError, Time.deltaTime);
        if (target.transform.position.z < transform.position.z)
            UpdatePitch(-heading.x);
            //pitch = -heading.x;
        else
            UpdatePitch(heading.x);
        //pitch = heading.x;
        UpdateYaw(heading.y);
        //yaw = heading.y;
        //roll = headingCorrection.z;
        //rigidbody.AddTorque(headingCorrection);
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

