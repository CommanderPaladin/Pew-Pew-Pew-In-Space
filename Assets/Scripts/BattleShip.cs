using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleShip : MonoBehaviour
{
    // Start is called before the first frame update
    Entity thisShip;
    int team = -1;
    bool shield;
    GameObject shieldObject = null;
    public GameObject nextLevel = null;
    void Start()
    {
        thisShip = this.GetComponent<Entity>();
        if (thisShip != null)
            team = thisShip.team;
        shield=true;
        shieldObject = this.gameObject.transform.Find("Shield").gameObject;
        shieldObject.SetActive(true);
        
    }

    public void SpawnNextLevel()
    {
        if (nextLevel != null)
        {
            GameObject portal = Instantiate(nextLevel, this.transform);
            portal.transform.parent = null;
        }

    }
    // Update is called once per frame
    private void Update()
    {
        //Debug.Log("Team: " + LevelControl.currentTeamMembers + "\n" + "Enemy: " + LevelControl.currentEnemyMembers);
        if (team == 1)
        {
            if (LevelControl.currentEnemyMembers <= 1 || LevelControl.wave >= 3)
            {
                shield = false;
                //shieldObject.SetActive(shield);
            }

            else
            {
                shield = true;
               // shieldObject.SetActive(shield);
            }

        }
        if (team == 2)
        {

            if (LevelControl.currentTeamMembers <= 1)
            {
                shield = false;
                //shieldObject.SetActive(shield);
            }

            else
            {
                shield = true;
                //shieldObject.SetActive(shield);
            }

        }
        shieldObject.SetActive(shield);
    }

    public void SetShield(bool val)
    {
        shield = val;
    }

    public bool GetShield()
    {
        return shield;
    }
}
