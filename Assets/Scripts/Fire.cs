using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fire : MonoBehaviour
{
    //this.transform.GetChild(0); -> Weapons
    public GameObject bulletType;
    public float cooldown = 1;
    public int bulletSpeed = 10000;

    private GameObject[] guns = new GameObject[2];
    private GameObject weaponsModule;

    private int leftRightFire = 0;
    private AIInput AIMovement;
    private bool isPlayer;
    private float reload = 0.0f;
    private Collider thisCollider;
    private Entity thisEntity;
    private ShipSounds thisSounds;
    // Start is called before the first frame update
    void Start()
    {
        thisEntity = this.GetComponent<Entity>();
        thisCollider = this.GetComponent<Collider>();
        if (this.GetComponent<Player>()!=null)
        {
            isPlayer = this.GetComponent<Player>().isPlayer; //check if this exists
        }
        else
        {
            AIMovement = this.GetComponent<AIInput>();
        }
        weaponsModule=this.transform.GetChild(0).gameObject.transform.Find("Weapons").gameObject;//ShipModel -> Weapons
        if (weaponsModule.name != null) //If Weapon Module exists
        {
            guns[0] = weaponsModule.transform.GetChild(0).gameObject;//Left
            guns[1] = weaponsModule.transform.GetChild(1).gameObject;//Right
        }
        else
            Debug.LogError("On " + gameObject.name + " there is something wrong with the Weapon module!");
        if (bulletType == null)
            Debug.LogWarning("There is no Bullet Type to shot.");

        thisSounds = this.gameObject.GetComponent<ShipSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            //0 - Left Click, 1 - Right Click, 2 - Middle Click
            if (Input.GetMouseButton(1)) 
            {
                if (reload>=cooldown)
                {
                    ShotBalistic();
                    reload = 0;

                    //Sound
                    if (thisSounds != null)
                        thisSounds.Fire();
                }

            }
        }
        else
        {
            //Timer
            //Fire if player in range (Ray from movement)
            RaycastHit ray = AIMovement.Ray();
            Entity entity = null;
            if (ray.collider != null)          
                entity = ray.collider.gameObject.GetComponentInParent<Entity>();
            
            if (ray.collider !=null&& entity !=null && entity.isShip==true && entity.team!= 0 && entity.team != thisEntity.team)
            {
                if (reload >= cooldown)
                {
                    ShotBalistic();
                    reload = 0;

                    //Sound
                    if (thisSounds != null)
                        thisSounds.Fire();
                }

            }
        }
        reload += 0.1f;
    }
    private void ShotBalistic()
    {
        GameObject bullet = Instantiate(bulletType, guns[leftRightFire].transform.position, transform.rotation);
        Physics.IgnoreCollision(bullet.transform.GetChild(0).GetComponent<Collider>(), transform.GetChild(0).GetComponent<Collider>());
        Rigidbody bulletGb = bullet.GetComponent<Rigidbody>();
        bulletGb.AddForce(transform.forward * bulletSpeed);
        Destroy(bullet, 3f);
        if (leftRightFire == 0)
            leftRightFire = 1;
        else
            leftRightFire = 0;
    }
}
