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
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (player != null)
        {
            LookAtPlayer();
            //Follow Player
            //Shot Player

            //A se folosi daca nu rezolvam cum trebuie
            //AIFailSafe();
        }

    }
    private void LookAtPlayer()
    {
        //Basic
        float offset = 10;
        //Test Area
        //rotatia pe y tre sa fie = 100-player.transform.x
        Vector3 playerPoz = (player.transform.position - this.transform.position);
        /*if (playerPoz.x > this.transform.rotation.y+offset && playerPoz.x < this.transform.rotation.y+offset)
        {
            yaw = 0;
        }
        else
        {
            if (playerPoz.x > this.transform.position.x)
            {
                UpdateYaw(0.1f);
            }
            else
            {
                UpdateYaw(-0.1f);
            }
        }*/


        /*if (player.transform.position.y-offset<this.transform.position.y)
        {
            UpdatePitch(0.1f);
        }
        else if (player.transform.position.y-offset > this.transform.position.y)
        {
            UpdatePitch(-0.1f);
        }
        else
        {
            //Test
            pitch = 0;
        }*/
        Debug.Log("Poz " + (player.transform.position - this.transform.position));
        //Debug.Log("Poz " + (this.transform.position - player.transform.position));

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
