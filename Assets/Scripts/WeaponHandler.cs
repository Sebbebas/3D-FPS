using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState
{
    Unarmed = 0,
    HitScan = 1,
    Projectile = 2,
    Rifle = 3,
    Total
}

public class WeaponHandler : MonoBehaviour
{
    [Header("Unarmed = Element 0 \n" + 
        "Hitscan = Element 1 \n" +
        "Projectile = Element 2")]
    public Weapon[] AvailableWeapons = new Weapon[(int)WeaponState.Total];
    public Weapon CurrentWeapon = null;
    public float mouseScrollDistance = 1f;
    private float mouseAxisDelta = 0.0f;

    private void Update()
    {
        HandleWeaponSwap();
        ActivateWeapon();

        if (Input.GetMouseButtonDown(0) && CurrentWeapon != null)
        {
            CurrentWeapon.Fire();
        }
    }

    private void ActivateWeapon()
    {
        foreach (var weapon in AvailableWeapons)
        {
            if (CurrentWeapon == weapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }


    private void HandleWeaponSwap()
    {
        mouseAxisDelta += Input.mouseScrollDelta.y;
        if (Mathf.Abs(mouseAxisDelta) > mouseScrollDistance)
        {
            int swapDirection = (int)Mathf.Sign(mouseAxisDelta);
            mouseAxisDelta -= swapDirection * mouseScrollDistance;

            int currentWeaponIndex = (int)CurrentWeapon.WeaponType;
            currentWeaponIndex += swapDirection;

            if (currentWeaponIndex < 0)
            {
                currentWeaponIndex = (int)WeaponState.Total + currentWeaponIndex;
            }
            if (currentWeaponIndex >= (int)WeaponState.Total)
            {
                currentWeaponIndex = 0;
            }
            CurrentWeapon = AvailableWeapons[currentWeaponIndex];
        }
    }
}
