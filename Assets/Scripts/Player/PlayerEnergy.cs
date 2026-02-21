using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public int MaxEnergy => maxEnergy;
    public int CurrentEnergy => currentEnergy;

    [SerializeField] private int maxEnergy = 100;

    private int currentEnergy;

    public void RestorEnergy(int amount)
    {
        currentEnergy = amount;
    }

    public void RechargeEnergy(int amount)
    {
        currentEnergy += amount;
        if(currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
    }

    public void UseEnergy(int amount)
    {
        currentEnergy -= amount;
        if(currentEnergy < 0)
        {
            currentEnergy = 0;
        }
    }

    public bool HasEnoughEnergy(int amount)
    {
        return currentEnergy >= amount;
    }
}
