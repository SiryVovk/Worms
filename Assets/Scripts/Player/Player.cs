using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovement playerMovement;

    public void RedyPlayerToTurn()
    {
        playerInput.enabled = true;
        playerMovement.enabled = true;

        playerMovement.RechargeEnergy(playerMovement.GetMaxEnergy());
    }

    public void StopPlayerTurn()
    {
        playerInput.enabled = false;
        playerMovement.enabled = false;
    }
}
