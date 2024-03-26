using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject weaponShotPos;

    public override bool Fire()
    {
        if (!base.Fire())
        {
            return false;
        }
        Debug.Log("Fire");
        FireProjectile();
        return true;
    }

    void FireProjectile()
    {
        Instantiate(projectile, weaponShotPos.transform.position, Quaternion.LookRotation(weaponShotPos.transform.forward));
    }
}
