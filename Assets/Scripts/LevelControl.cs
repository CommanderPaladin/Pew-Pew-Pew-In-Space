using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{

    //Base
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    //How many ships
    public int teamNumber;
    public int enemyNumber;

    //The prefabs
    public GameObject allyShip;
    public GameObject enemyShip;

    //After a spawning wave
    public int teamSpawnCooldown=100;
    public int enemySpawnCooldown=100;

    //Is in Spawning Phase
    private bool isSpawningAlly = false;
    private bool isSpawningEnemy = false;

    //The Clock
    private float cooldownAlly = 0;
    private float cooldownEnemy = 0;

    private float allySpawnPeriod = 36f;
    private float enemySpawnPeriod = 36f;

    public static int currentTeamMembers = 0;
    public static int currentEnemyMembers = 0;

    public static int wave = 0;
    void Start()
    {
        //Get Current Team Members
        //Get Current Enemy Members
        Entity[] entities = Object.FindObjectsOfType<Entity>(); 
        for (int i = 0; i < entities.Length;i++)
        {
            if (entities[i].team == 1)
                LevelControl.currentEnemyMembers += 1;
            if (entities[i].team == 2)
                LevelControl.currentTeamMembers += 1;
        }
        //Debug.Log("There are " + currentEnemyMembers + " enemies and " + currentTeamMembers + " allies");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("There are " + currentEnemyMembers + " enemies and " + currentTeamMembers + " allies");
        if (spawnPoint1!=null || spawnPoint2!=null)
        {
            if (spawnPoint1!=null) //Ally
            {
                if (isSpawningAlly)
                {
                    if (allySpawnPeriod <= cooldownAlly)
                    {
                        Spawn(spawnPoint1, allyShip); //SPAWN IN ALLY BASE
                        cooldownAlly = 0;
                    }
                    if (currentTeamMembers >= teamNumber)
                        isSpawningAlly = false;
                }
                else if (isSpawningAlly == false && cooldownAlly >= teamSpawnCooldown && currentTeamMembers < teamNumber)
                {
                    isSpawningAlly = true;
                }
            }
            if (spawnPoint2!=null) //Enemy
            {
                if (isSpawningEnemy)
                {
                    if (enemySpawnPeriod <= cooldownEnemy)
                    {
                        Spawn(spawnPoint2, enemyShip); //SPAWN IN ENEMY BASE
                        cooldownEnemy = 0;
                    }
                    if (currentEnemyMembers >= enemyNumber)
                        isSpawningEnemy = false;
                }
                else if (isSpawningEnemy == false && cooldownEnemy >= enemySpawnCooldown && currentEnemyMembers < enemyNumber)
                {
                    wave++;
                    isSpawningEnemy = true;
                }
            }
            

        }
        cooldownAlly += 0.2f;
        cooldownEnemy += 0.2f;

    }
    private void Spawn(Transform atBase, GameObject ship)
    {
        GameObject spawned = Instantiate(ship,atBase.position,atBase.rotation);
        if (atBase == spawnPoint1)
            currentTeamMembers += 1;
        if (atBase == spawnPoint2)
            currentEnemyMembers += 1;
        //Etc etc
    }
}
