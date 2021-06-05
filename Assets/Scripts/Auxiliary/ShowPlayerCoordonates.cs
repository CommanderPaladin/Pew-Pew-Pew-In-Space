using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlayerCoordonates : MonoBehaviour
{
    private Transform player;
    private bool active = false;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            active = !active;
        }
        if (active==true)
        {
            if (player != null)
            {
                Debug.Log(player.position);
            }
        }
    }
}
