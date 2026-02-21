using UnityEngine;

public class Granade : MonoBehaviour, IWeapon
{
    [SerializeField] private Transform spawnPoint;

    private SpawnblObjects granadeObject;
    private ProjectileStats projectileStats;

    public void Init(WeaponSO weaponSO)
    {
        granadeObject = weaponSO.SpawnblObjects;
        projectileStats = weaponSO.ProjectileStats;
    }

    public void Launch(LaunchContext launchContext)
    {

    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

}
