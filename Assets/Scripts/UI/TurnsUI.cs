using UnityEngine;

public class TurnsUI : MonoBehaviour
{
    public void EndTurn()
    {
        TurnsManagment.Instance.EndTurn();
    }
}
