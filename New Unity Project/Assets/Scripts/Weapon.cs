using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] WeaponData weaponData;

    float timeSinceLastShot;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
    }

    private bool CanShoot() => !weaponData.reloading && timeSinceLastShot > 1f / (weaponData.fireRate / 60f);

    public void Shoot()
    {
        if (weaponData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, weaponData.range))
                {
                    Debug.Log(hitInfo.transform.name);
                }

                weaponData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    private void OnGunShot()
    {
        //throw new NotImplementedException();
    }

}
