using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public bool visible = true;
    private GameObject hud;
    void Start()
    {
        hud = GameObject.Find("HUD");
    }

    // Update is called once per frame
    void Update()
    {
        if (hud!=null)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                visible = !visible;
                hud.gameObject.SetActive(visible);
            }
            
        }
    }
}
