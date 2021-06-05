using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public bool isShip = false;
    public int team = 0;
    public int health=100;

    //[Tooltip("Meow")]
    [Header("Particles")]
    public GameObject hitEffect;
    public GameObject deathEffect;


    //DOAR DE TEST!
    private void OnCollisionEnter(Collision other)
    {
        Entity otherEntity = other.gameObject.GetComponentInParent<Entity>();
        if (otherEntity != null )
        {
            //QuickFix bulet - other objects
            if (otherEntity.gameObject.name.Contains("Bullet") && !(this.gameObject.name.Contains("Bullet")))
            {

                  BattleShip battleShip = this.GetComponent<BattleShip>();

                //Bullet = Ships/Other objects
                if (battleShip == null)
                    this.health -= 1;
                else
                {
                    if (battleShip.GetShield() == false) //Battleships
                       this.health -= 1;
                }


                //Sound
                ShipSounds otherSound = this.gameObject.GetComponentInParent<ShipSounds>();
                if (otherSound != null)
                    otherSound.Hit();

                //Particle
                if (hitEffect!=null)
                {
                    GameObject hitEffectParcticle = Instantiate(hitEffect, other.transform.position, other.transform.rotation);
                    //Physics.IgnoreCollision(hitEffect.transform.GetChild(0).GetComponent<Collider>(), transform.GetChild(0).GetComponent<Collider>());
                    //Rigidbody hitEffectParcticle = hitEffect.GetComponent<Rigidbody>();
                    //hitEffectParcticle.AddForce(-transform.forward * 0);
                    Destroy(hitEffectParcticle, 1f);
                }


            }
            //Impact ship - ship
            if (otherEntity.isShip == true && otherEntity.team != this.team && otherEntity.team!=1 && otherEntity.team != 2 && otherEntity.GetComponent<BattleShip>() == null) 
            {
                otherEntity.health -= 1;
            }
            else
            {
                //Add other rules FOR ENTITIES OBJECTS,
                //Example: Bullet to asteroid rock, etc
            }
        }
        else
        {
            //Add other rules for OBJECTS THAT COLLIDE BUT ARE NOT ENTITIES
        }


        if (other.gameObject.name.Contains("Rock") && !(this.gameObject.name.Contains("rock") || this.gameObject.name.Contains("Rock"))) //Big rock turns to little
        {
            other.gameObject.GetComponent<Fracture>().FractureObject();
        }
        if (this.gameObject.name=="Bullet(Clone)") //Destroy bullet on impact
            Destroy(this.gameObject);

        CheckLife();
    }
    void CheckLife()
    {
        if (health<=0)
        {
            Entity entity = this.gameObject.GetComponent<Entity>();
            if (entity!=null)
            {
                if (entity.isShip || entity.gameObject.name.Contains("BattleShip") )
                {
                    if (entity.team == 1) //Enemy
                    {
                        GameControl.Money += 10;
                        GameControl.Score += 1000;
                        //Debug.Log(GameControl.Money);
                        //Debug.Log(GameControl.Score);
                        LevelControl.currentEnemyMembers -= 1;
                        
                        if(entity.gameObject.name.Contains("BattleShip"))
                        {
                            BattleShip bs = entity.gameObject.GetComponent<BattleShip>();
                            if (bs != null)
                                bs.SpawnNextLevel();
                        }
                    }

                    else if (entity.team == 2) //Team
                    {
                        LevelControl.currentTeamMembers -= 1;
                        if (entity.gameObject.GetComponent<Player>()!=null)
                        {
                            GameControl.DeathScene();
                        }
                    }
                }       
            }
            if (deathEffect!=null)
            {
                StartCoroutine(Die());
            }


            Destroy(this.gameObject);
        }
    }
    IEnumerator Die()
    {
        GameObject death = Instantiate(deathEffect, transform.position, transform.rotation);
        ShipSounds sounds = this.gameObject.GetComponent<ShipSounds>();
        if (sounds != null)
        {
            if (sounds.explosion != null)
            {
                AudioSource tst = death.AddComponent<AudioSource>();
                tst.clip = sounds.explosion;
                tst.rolloffMode = AudioRolloffMode.Linear;
                tst.maxDistance = 400;
                tst.spatialBlend = 1;
                tst.volume = 0.8f;
                tst.Play();
            }
        }

        Destroy(death, 1.2f);
        yield return 1.2f;
    }
}
