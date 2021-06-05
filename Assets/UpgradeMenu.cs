using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public static int RateofFire=1;
    public static int Damage=1;
    public static int Health=1;
    public static int Speed=1;

    private static int price =50;

    private void Start()
    {
        Cursor.visible = true;
    }
    public static void NextLevel()
    {
        GameControl.NextLevel();
    }
    public static void IncrementRateofFire()
    {
        if (GameControl.Money>=price)
        {
            GameControl.Money -= price;
            RateofFire += 1;

            if (RateofFire > 5)
                RateofFire = 5;
        }
    }

    public static void IncrementDamage()
    {

        if (GameControl.Money >= price)
        {
            GameControl.Money -= price;
            Damage += 1;

            if (Damage > 5)
                Damage = 5;
        }

    }

    public static void IncrementHealth()
    {
        if (GameControl.Money >= price)
        {
            GameControl.Money -= price;
            Health += 1;

            if (Health > 5)
                Health = 5;
        }

    }

    public static void IncrementSpeed()
    {
        if (GameControl.Money >= price)
        {
            GameControl.Money -= price;
            Speed += 1;

            if (Speed > 5)
                Speed = 5;
        }

    }

    public static void DecrementRateofFire()
    {
        RateofFire -= 1;
        if (RateofFire < 1)
            RateofFire = 1;
        if (RateofFire>1)
            GameControl.Money += price;
    }

    public static void DecrementDamage()
    {
        Damage -= 1;
        if (Damage < 1)
            Damage = 1;
        if (Damage>1)
            GameControl.Money += price;
    }

    public static void DecrementHealth()
    {
        Health -= 1;
        if (Health < 1)
            Health = 1;
        if (Health>1)
            GameControl.Money += price;
    }

    public static void DecrementSpeed()
    {
       
        Speed -= 1;
        if (Speed < 1)
            Health = 1;
        if (Speed>1)
            GameControl.Money += price;
    }

}
