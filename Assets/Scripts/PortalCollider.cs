using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.GetComponentInParent<Player>() != null)
        {
            GameControl.NextLevel();
        }
    }
}
