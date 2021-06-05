using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    ShipSounds ss;

    /*IEnumerator PlayStartSound()
    {
        if (ss != null)
        {
            ss.StartSound();
        }
        yield return new WaitForSeconds(0.2f);

    }
    */
    void Start()
    {
        ss = this.GetComponent<ShipSounds>();
        //PlayStartSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopAnimation()
    {
        Animator an = this.GetComponent<Animator>();
        if (an!=null)
        {
            an.StopPlayback();
            an.speed = 0;

            ParticleSystem smoke = this.gameObject.GetComponentInChildren<ParticleSystem>();
            if (smoke!=null)
                smoke.maxParticles = 0;
            if (ss!=null)
            {
                ss.StopStartSound();
            }
        }

    }
}
