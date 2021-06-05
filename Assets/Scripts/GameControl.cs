using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public static int Money
    {
        get;set;
    }
    
    public static int Score
    {
        get;set;
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void DeathScene()
    {
        SceneManager.LoadScene("Death Scene");
    }
    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    /*void Update()
    {
        
    }*/


}
