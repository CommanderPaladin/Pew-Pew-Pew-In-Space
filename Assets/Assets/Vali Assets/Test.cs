using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>()!=null)
        {
            GameControl.NextLevel();
        }
    }
    // Update is called once per frame

}
