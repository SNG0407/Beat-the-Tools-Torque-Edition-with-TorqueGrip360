using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarEffectManger : MonoBehaviour
{
    public bool playAura = true; //파티클 제어 bool
    public ParticleSystem WFX_19_StarTail_GazFire;
    public ParticleSystem WFX_4_StarDestroy_Explosion;
    public ParticleSystem WFX_6_StarDestroy_ExplosiveSmoke;
    public ParticleSystem WFX_3_Groud_ExplosionLandMine;

    void Start()
    {
        playAura = true;
        WFX_19_StarTail_GazFire.Play();
    }


    void Update()
    {
        // if (playAura)                    
        //     particleObject.Play();       
        // else if (!playAura)                
        //     particleObject.Stop();        
    }

    public void Effect_StarDestroy_Explosion()
    {
        WFX_4_StarDestroy_Explosion.Play();
    }
    public void Effect_WFX_6_StarDestroy_ExplosiveSmoke()
    {
        WFX_6_StarDestroy_ExplosiveSmoke.Play();
    }
    public void Effect_WFX_3_Groud_ExplosionLandMine()
    {
        WFX_3_Groud_ExplosionLandMine.Play();
    }

}