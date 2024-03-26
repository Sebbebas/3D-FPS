using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Configurabel Perameters
    [Header("Type")]
    public WeaponState WeaponType = WeaponState.Total;

    [Header("Ammo")]
    public int currentAmmo;
    public int totalAmmo = 90;
    public int magSize = 30;

    [Header("Extra")]
    public LayerMask ignoreHitMask = 0;
    public float weaponRange = 100f;
    public float reloadTime = 3;
    protected Camera mainCam = null;

    [Header("Effects")]
    public ParticleSystem hitParticle;
    public Animator animator;

    //Private Variabels

    protected void Start()
    {
        mainCam = Camera.main;

        if(currentAmmo == 0)
        {
            currentAmmo = magSize;
        }
    }

    private void Update()
    {
        
    }

    public void Reload()
    {
        if (currentAmmo == magSize && totalAmmo == 0) { Debug.Log("cant reload"); return; }

        animator.SetBool("Reload", true);

        int ammoToAdd = magSize - currentAmmo;
        
        if (totalAmmo >= ammoToAdd)
        {
            currentAmmo = magSize;
            totalAmmo -= ammoToAdd;
        }
        else
        {
            currentAmmo += currentAmmo;
            currentAmmo = 0;
        }
    }

    public virtual bool Fire()
    {
        //false
        if(currentAmmo < 1)
        {
            return false;
        }
        //true
        currentAmmo--;
        animator.SetBool("Fire", true);

        //no ammo left in mag
        if (currentAmmo == 0)
        {
            Reload();
        }
        return true;
    }
}
