using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Gun")]
public class WeaponData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Firing")]
    public float damage;
    public float range;

    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    public float fireRate;
    public float reloadTime;

    [HideInInspector]
    public bool reloading;
}
