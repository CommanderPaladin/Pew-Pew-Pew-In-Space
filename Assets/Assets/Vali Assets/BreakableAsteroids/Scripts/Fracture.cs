using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    [Tooltip("\"Fractured\" is the object that this will break into")]
    public GameObject fractured;

    public void FractureObject()
    {
        GameObject fracturedObject = Instantiate(fractured, transform.position, transform.rotation); //Spawn in the broken version
        fracturedObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        
        // Best code ever !!!!
        
         for (int i = 0; i < fracturedObject.transform.childCount;i++)
        {
            GameObject kid = fracturedObject.transform.GetChild(i).gameObject;
            Entity entityKid = kid.AddComponent<Entity>();
            entityKid.team = -1;
           // entityKid.health = 2;
            Rigidbody rb = kid.GetComponent<Rigidbody>();
            rb.useGravity = false;
            //rb.velocity = this.gameObject.GetComponent<Rigidbody>().velocity;
            //rb.AddForce(this.gameObject.GetComponent<Rigidbody>().velocity);
            //transform.RotateAround(this.transform.position, parent.transform.up, orbitSpeed * Time.deltaTime);


        }
        

        Destroy(gameObject); //Destroy the object to stop it getting in the way
    }
}
