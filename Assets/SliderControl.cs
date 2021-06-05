using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    // Start is called before the first frame update
    Slider slide = null;
    void Start()
    {
        slide = this.GetComponent<Slider>();
        if (slide!=null)
        {
            if (this.gameObject.name.Contains("Damage"))
                slide.value = UpgradeMenu.Damage;
            if (this.gameObject.name.Contains("Health"))
                slide.value = UpgradeMenu.Health;
            if (this.gameObject.name.Contains("RateofFire"))
                slide.value = UpgradeMenu.RateofFire;
            if (this.gameObject.name.Contains("Speed"))
                slide.value = UpgradeMenu.Speed;


        }

    }

    public void IncremetValue()
    {
        if (slide != null)
        {
            if (this.gameObject.name.Contains("Damage"))
                slide.value = UpgradeMenu.Damage;
            if (this.gameObject.name.Contains("Health"))
                slide.value = UpgradeMenu.Health;
            if (this.gameObject.name.Contains("RateofFire"))
                slide.value = UpgradeMenu.RateofFire;
            if (this.gameObject.name.Contains("Speed"))
                slide.value = UpgradeMenu.Speed;


        }
        /*if (slide!=null)
        {
            slide.value += 1;
            if (slide.value>5)
            {
                slide.value = 5;
            }
        }*/

    }

    public void DecrementValue()
    {
        if (slide != null)
        {
            if (this.gameObject.name.Contains("Damage"))
                slide.value = UpgradeMenu.Damage;
            if (this.gameObject.name.Contains("Health"))
                slide.value = UpgradeMenu.Health;
            if (this.gameObject.name.Contains("RateofFire"))
                slide.value = UpgradeMenu.RateofFire;
            if (this.gameObject.name.Contains("Speed"))
                slide.value = UpgradeMenu.Speed;


        }
        /*if (slide!=null)
        {
            slide.value -= 1;
            if (slide.value <1)
            {
                slide.value = 1;
            }
        }*/
    }

    // Update is called once per frame
   
}
