using System.Collections.Generic;
using UnityEngine;

public class TurnsManagment : MonoBehaviour
{
    public static TurnsManagment Instance => instance;

    private static TurnsManagment instance;

    private Queue<Player> players = new Queue<Player>();

    private Player currentPlayer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        players = new Queue<Player>(FindObjectsByType<Player>(FindObjectsSortMode.None));

        if (players.Count == 0)
        {
            Debug.LogError("No players assigned to TurnsManagment!");
        }
        else
        {
            StartFirstTurn();
        }
    }

    private void StartFirstTurn()
    {
        currentPlayer = players.Dequeue();
        currentPlayer.RedyPlayerToTurn();
    }

    public void EndTurn()
    {
        currentPlayer.StopPlayerTurn();
        players.Enqueue(currentPlayer);
        currentPlayer = players.Dequeue();
        currentPlayer.RedyPlayerToTurn();
    }
}
