using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTextUpgradeMenu : MonoBehaviour
{
    // Start is called before the first frame update
    Text text = null;
    void Start()
    {


        text = this.GetComponent<Text>();


        //text.text = "CACA";
    }
    private void Update()
    {
        string textBuild = "Money: " + GameControl.Money;
        text.text = textBuild;
    }
    // Update is called once per frame

}
