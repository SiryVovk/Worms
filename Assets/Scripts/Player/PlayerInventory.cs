using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public event Action OnInventoryOpened;

    [SerializeField] private WeaponSO[] weapons;
    [SerializeField] private Transform weaponSpawnPoint;

    private PlayerInput playerInput;

    private WeaponSO equipedWeapon;
    private IWeapon equipedWeaponInstance;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.OnInventory += HandleInventory;
    }

    private void OnDestroy()
    {
        playerInput.OnInventory -= HandleInventory;
    }

    private void HandleInventory()
    {
        OnInventoryOpened?.Invoke();
    }

    public WeaponSO[] GetWeapons()
    {
        return weapons;
    }
    
    public void EquipWeapon(WeaponSO weapon)
    {
        equipedWeapon = weapon;
        HandleInventory();
        if (equipedWeaponInstance != null)
        {
            equipedWeaponInstance.Destroy();
        }

        equipedWeaponInstance = Instantiate(weapon.SpawnblObjects.weaponObject, weaponSpawnPoint).GetComponent<IWeapon>();
        equipedWeaponInstance.Init(weapon);
    }
}
