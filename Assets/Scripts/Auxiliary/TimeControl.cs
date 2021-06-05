using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("]"))
        {
            Time.timeScale += 2f;
        }
        else if (Input.GetKeyDown("["))
        {
            Time.timeScale -= 1f;
            if (Time.timeScale <= 1)
                Time.timeScale = 1;
        }
        else if (Input.GetKeyDown("\\"))
        {
            Time.timeScale = 1f;
        }
        else if (Input.GetKeyDown("'"))
        {
            Time.timeScale = 0f;
        }

    }
}
