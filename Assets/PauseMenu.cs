using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    GameObject miniMenu;
    MouseFollow movingCross;
    bool active = false;
    private void Start()
    {
        miniMenu = this.transform.Find("MiniMenu").gameObject;
        GameObject movingCrossOBJ = this.transform.Find("MovingCrosshair").gameObject;
        if (movingCrossOBJ != null)
        {
            movingCross = movingCrossOBJ.GetComponent<MouseFollow>();
        }
        if (miniMenu!=null)
        {
            miniMenu.SetActive(false);
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        GameControl.LoadMenu();
    }

    public void CloseGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    public void Resume()
    {
        movingCross.SetPause(false);
        miniMenu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            active = !active;
            if (active == true)
            {
                Time.timeScale = 0;
                if (miniMenu!=null)
                {
                    movingCross.SetPause(true);
                    miniMenu.SetActive(true);
                    Cursor.visible = true;
                }

            }

            else
            {
                Time.timeScale = 1;
                if (miniMenu != null)
                {
                    movingCross.SetPause(false);
                    miniMenu.SetActive(false);
                    Cursor.visible = false;
                }

            }

        }
    }
}
