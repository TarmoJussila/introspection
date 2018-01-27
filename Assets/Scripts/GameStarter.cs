using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game starter. Attach to button.
/// </summary>
public class GameStarter : MonoBehaviour
{
    // Start game.
    public void StartGame()
    {
        GameController.Instance.StartGame();
    }
}