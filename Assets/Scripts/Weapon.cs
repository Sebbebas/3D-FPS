using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponState WeaponType = WeaponState.Total;
    public int Ammunition = 0;

    public float weaponRange = 100f;
    public LayerMask ignoreHitMask = 0;
    public ParticleSystem hirParticle;
    
    protected Camera mainCam = null;

    protected void Start()
    {
        mainCam = Camera.main;
    }

    public virtual bool Fire()
    {
        if(Ammunition < 1)
        {
            return false;
        }
        Ammunition--;
        return true;
    }
}
