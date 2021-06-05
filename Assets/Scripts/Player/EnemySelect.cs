using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelect : MonoBehaviour
{
    [HideInInspector]
    public GameObject selected;
    public void SelectEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if (Physics.Raycast (ray,out rayHit,10000))
        {
            if (rayHit.collider != null)
            {
                //Debug.Log(rayHit.collider.name);
                if (rayHit.transform.gameObject.name.Contains("Enemy"))
                {
                    selected = rayHit.transform.gameObject;
                    Debug.Log(selected.name);
                }
            }
               

        }
    }
}
