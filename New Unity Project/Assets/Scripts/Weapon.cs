using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform muzzle;
    [SerializeField] public Text ammoDisplay;

    float timeSinceLastShot;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    public void StartReload()
    {
        if (!weaponData.reloading)
        {
            StartCoroutine(Reload());
        }
    }
    
    private IEnumerator Reload()
    {
        weaponData.reloading = true;

        yield return new WaitForSeconds(weaponData.reloadTime);

        weaponData.currentAmmo = weaponData.magSize;

        weaponData.reloading = false;
    }

    private bool CanShoot() => !weaponData.reloading && timeSinceLastShot > 1f / (weaponData.fireRate / 60f);

    public void Shoot()
    {
        if (weaponData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if (Physics.Raycast(muzzle.position, transform.forward, out RaycastHit hitInfo, weaponData.range))
                {
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.Damage(weaponData.damage);
                }

                weaponData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

    private void Update()
    {
        ammoDisplay.text = weaponData.currentAmmo.ToString();
        timeSinceLastShot += Time.deltaTime;
    }

    private void OnGunShot()
    {
        //throw new NotImplementedException();
    }

}
