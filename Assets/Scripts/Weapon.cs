using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    //Configurable Perameters
    [Header("Type")]
    public WeaponState WeaponType = WeaponState.Total;

    [Header("Ammo")]
    public int currentAmmo;
    public int totalAmmo = 90;
    public int magSize = 30;
    public TextMeshProUGUI ammoText;

    [Header("Extra")]
    public LayerMask ignoreHitMask = 0;
    public float weaponRange = 100f;
    protected Camera mainCam = null;

    [Header("Delays")]
    public float reloadTime = 3f;
    public float firedelay = 3f;

    [Header("Effects")]
    public ParticleSystem hitParticle;
    public Animator animator;

    //Private Variabels
    private float currentFireTime;
    private float currentReloadTime;

    protected void Start()
    {
        mainCam = Camera.main;

        if (currentAmmo == 0)
        {
            currentAmmo = magSize;
        }
    }

    private void Update()
    {
        //Reload
        if (currentAmmo == 0)
        {
            Reload();
        }

        if (gameObject.activeSelf) { ammoText.text = currentAmmo.ToString() + "/" + totalAmmo.ToString(); }

        //Cooldowns
        if (currentFireTime > 0) { currentFireTime -= Time.deltaTime; }
        else { currentFireTime = 0; if (animator != null) { animator.SetBool("Fire", false); } }

        if (currentReloadTime > 0) { currentReloadTime -= Time.deltaTime; }
        else { currentReloadTime = 0; if (animator != null) { animator.SetBool("Reload", false); } }
    }

    public void Reload()
    {
        if (currentAmmo == magSize && totalAmmo == 0) { Debug.Log("cant reload"); return; }
        if (animator != null) { animator.SetBool("Reload", true); }

        StartCoroutine(ReloadRoutine());
        currentReloadTime = reloadTime;
    }

    public IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(reloadTime);
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

        StopCoroutine(ReloadRoutine());
    }

    public virtual bool Fire()
    {
        //false
        if (currentAmmo == 0 || currentFireTime > 0 && currentAmmo != 0 || currentReloadTime > 0)
        {
            if (currentAmmo == 0)
            {
                Reload();
            }
            return false;
        }
        //true
        currentAmmo--;
        currentFireTime = firedelay;
        if (animator != null) { animator.SetBool("Fire", true); } //Null Check can be simplified
        return true;
    }
}