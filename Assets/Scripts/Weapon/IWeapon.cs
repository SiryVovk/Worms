public interface IWeapon
{
    public void Launch(LaunchContext launchContext);
    public void Init(WeaponSO weaponSO);

    public void Destroy();
}
