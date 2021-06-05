using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseFollow : MonoBehaviour
{
    private Image crosshair;
    bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        crosshair = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused==false)
        {
            if (crosshair != null && Player.PlayerShip != null)
            {
                if (crosshair.enabled)
                {
                    crosshair.transform.position = Input.mousePosition;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Confined;
                }
                else
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
    }

    public void SetPause(bool pa)
    {
        isPaused = pa;
    }
}
