using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class WeaponSO : ScriptableObject
{
    public WeaponTypes WeaponTypes;

    public WeaponUI WeaponUI;
    public Consamtion Consamtion;
    public SpawnblObjects SpawnblObjects;
    public ProjectileStats ProjectileStats;
}

[Serializable]
public struct WeaponUI
{
    public Sprite UIWeaponSprite;
}

[Serializable]
public struct Consamtion
{
    public int launchCost;
}

[Serializable]
public struct SpawnblObjects
{
    public GameObject weaponObject;
    public GameObject projectileObject;
}

[Serializable]
public struct ProjectileStats
{
    public float launchForceMin;
    public float launchForceMax;
    public float lifetime;
    public float explosionRadius;
    public float explosionDamage;
}

