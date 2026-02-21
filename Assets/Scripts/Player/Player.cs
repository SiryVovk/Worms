using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private PlayerEnergy playerEnergy;

    public void RedyPlayerToTurn()
    {
        playerInput.enabled = true;
        playerMovement.enabled = true;
        playerInventory.enabled = true;

        playerEnergy.RestorEnergy(playerEnergy.MaxEnergy);
    }

    public void StopPlayerTurn()
    {
        playerInput.enabled = false;
        playerMovement.enabled = false;
        playerInventory.enabled = false;
    }
}
