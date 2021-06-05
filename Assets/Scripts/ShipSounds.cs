using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSounds : MonoBehaviour
{
    public AudioClip engine;
    public AudioClip fire;
    public AudioClip boost;
    public AudioClip explosion;
    public AudioClip startSound;
    public AudioClip []hit = new AudioClip[2];


    bool isPlayer = false;

    AudioSource audioEngine = null;
    AudioSource audioFire = null;
    AudioSource audioHit = null;
    AudioSource audioBoost = null;
    AudioSource audioStartSound = null;
    [HideInInspector]
    public AudioSource audioExplosion = null;

    private void Start()
    {
        Player p = gameObject.GetComponent<Player>();
        if (p != null)
        {
            isPlayer = true;
            audioBoost = gameObject.AddComponent<AudioSource>();
            BoostInit();
        }


        audioEngine = gameObject.AddComponent<AudioSource>();
        EngineInit();

        audioFire = gameObject.AddComponent<AudioSource>();
        FireInit();


        audioHit = gameObject.AddComponent<AudioSource>();
        HitInit();

        audioExplosion = gameObject.AddComponent<AudioSource>();
        ExplosionInit();

        audioStartSound = gameObject.AddComponent<AudioSource>();
        StartSoundInit();
    }

    #region Boost
    IEnumerator BoostPlay()
    {
        audioBoost.Play();

        yield return null;
    }
    IEnumerator BoostStop()
    {
        audioBoost.Stop();

        yield return null;
    }

    public void PlayBoostSound()
    {
        if (audioBoost != null)
        {
            //if (audioBoost.isPlaying == false)
            //{
                StartCoroutine(BoostPlay());
            //}
            //else
            //{
                //audioBoost.Stop();
            //}
        }
    }
    public void StopBoostSound()
    {
        StartCoroutine(BoostStop());
    }
    private void BoostInit()
    {
        if (audioBoost != null)
        {
            if (isPlayer)
                audioBoost.volume = 0.15f;
            audioBoost.clip = boost;
            audioBoost.rolloffMode = AudioRolloffMode.Linear;
            audioBoost.maxDistance = 180;
            //audioBoost.loop = true;
            audioBoost.spatialBlend = 1;
            //PlayBoostSound();
        }
    }
    #endregion


    #region Engine
    IEnumerator EnginePlay()
    {
        audioEngine.Play();

        yield return null;
    }

    public void PlayEngineSound()
    {
        if (audioEngine != null)
        {
            if (audioEngine.isPlaying == false)
            {
                StartCoroutine(EnginePlay());
            }
            else
            {
                audioEngine.Stop();
            }
        }
    }
    private void EngineInit()
    {
        if (audioEngine!=null)
        {
            if (isPlayer)
                audioEngine.volume = 0.2f;
            audioEngine.clip = engine;
            audioEngine.rolloffMode = AudioRolloffMode.Linear;
            audioEngine.maxDistance = 180;
            audioEngine.loop = true;
            audioEngine.spatialBlend = 1;
            PlayEngineSound();
        }
    }
    #endregion

    #region Fire
    IEnumerator FirePlay()
    {
        audioFire.Play();
        yield return null;
    }
    private void FireInit()
    {
        if (audioFire!=null)
        {
            audioFire.volume = 0.1f;
            if (isPlayer)
                audioFire.volume = 0.1f;

            audioFire.clip = fire;
            audioFire.rolloffMode = AudioRolloffMode.Linear;
            audioFire.maxDistance = 250;
            audioFire.spatialBlend = 1;
        }
    }
    public void Fire()
    {
        StartCoroutine(FirePlay());
    }
    #endregion

    #region Hit

    IEnumerator HitPlay()
    {
        audioHit.Play();
        yield return null;
    }
    private void HitInit()
    {
        if (audioHit != null)
        {
            //audioHit.volume = 0.5f;
            if (isPlayer)
                audioHit.volume = 0.3f;

            audioHit.rolloffMode = AudioRolloffMode.Linear;
            audioHit.maxDistance = 400;
            audioHit.spatialBlend = 1;
        }
    }
    public void Hit()
    {
        audioHit.clip = hit[Random.Range(0, 1)];
        StartCoroutine(HitPlay());
    }
    #endregion

    #region Explosion
    IEnumerator ExplosionPlay()
    {
        audioExplosion.Play();
        yield return null;
    }
    private void ExplosionInit()
    {
        if (audioExplosion != null)
        {
            //audioExplosion.volume = 0.1f;
            //if (isPlayer)
            //    audioExplosion.volume = 2f;

            audioExplosion.clip = explosion;
            audioExplosion.volume = 0.75f;
            audioExplosion.rolloffMode = AudioRolloffMode.Linear;
            audioExplosion.maxDistance = 400;
            audioExplosion.spatialBlend = 1;
        }
    }
    public void Explosion()
    {
        StartCoroutine(ExplosionPlay());
        //StartCoroutine()
    }

    #endregion


    #region StartSound
    IEnumerator StartSoundPlay()
    {
        audioStartSound.Play();
        yield return null;
    }
    private void StartSoundInit()
    {
        if (audioStartSound != null)
        {
            //audioExplosion.volume = 0.1f;
            //if (isPlayer)
            //    audioExplosion.volume = 2f;

            audioStartSound.clip = startSound;
            audioStartSound.volume = 1f;
            audioStartSound.rolloffMode = AudioRolloffMode.Linear;
            audioStartSound.maxDistance = 1200;
            audioStartSound.spatialBlend = 1;
            StartSound();
        }
    }
    public void StartSound()
    {
        StartCoroutine(StartSoundPlay());
        //StartCoroutine()
    }

    public void StopStartSound()
    {
        //StartCoroutine(StartSoundPlay());
        audioStartSound.Stop();
        //StartCoroutine()
    }
    #endregion
    /*
     public AudioClip otherClip;

    IEnumerator Start()
    {
        AudioSource audio = GetComponent<AudioSource>();

        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = otherClip;
        audio.Play();
    }*/
}
